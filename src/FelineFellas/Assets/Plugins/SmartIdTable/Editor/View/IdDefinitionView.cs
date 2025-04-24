using System;
using UnityEngine.UIElements;

namespace SmartIdTable.Editor.View
{
	public class IdDefinitionView : VisualElement, INotifyValueChanged<string>
	{
		public new class UxmlFactory : UxmlFactory<IdDefinitionView, UxmlTraits> { }

		public const string BaseUSSClassName = "id-view";
		public const string InputFieldUSSClassName = BaseUSSClassName + "__input";
		public const string AddButtonUSSClassName = BaseUSSClassName + "__btn-add";
		public const string RemoveButtonUSSClassName = BaseUSSClassName + "__btn-remove";

		private const string CLASS_DISABLED = "leaf";

		private readonly TextField _textField;
		private readonly Foldout _foldout;
		private readonly VisualElement _checkmark;

		public event Action<IdDefinitionView> AddButtonClicked;
		public event Action<IdDefinitionView> RemoveButtonClicked;

		public IdDefinitionView() : this(string.Empty) { }

		public IdDefinitionView(string globalId)
		{
			GlobalId = globalId;
			_foldout = new Foldout { viewDataKey = globalId };
			_checkmark = _foldout.Q(className: Foldout.inputUssClassName);

			_foldout.RegisterCallback(new EventCallback<AttachToPanelEvent>(_ => OnChildCountChanged()));
			_foldout.RegisterCallback(new EventCallback<DetachFromPanelEvent>(_ => OnChildCountChanged()));

			AddToClassList(BaseUSSClassName);

			hierarchy.Add(_foldout);

			var foldoutToggle = _foldout.Q<Toggle>();
			var foldoutHeader = foldoutToggle.Q<Toggle>();

			_textField = new TextField
			{
				isDelayed = true,
				pickingMode = PickingMode.Ignore,
			};

			// _textField.RegisterValueChangedCallback(evt => value = evt.newValue);
			_textField.AddToClassList(InputFieldUSSClassName);

			var addButton = new Button(() => AddButtonClicked?.Invoke(this))
			{
				name = "btn__add",
				text = "+",
				tooltip = "Add child ID",
				displayTooltipWhenElided = true
			};
			addButton.AddToClassList(AddButtonUSSClassName);

			var removeButton = new Button(() => RemoveButtonClicked?.Invoke(this))
			{
				name = "btn__remove",
				text = "-",
				tooltip = "Remove this and child IDs",
				displayTooltipWhenElided = true
			};
			removeButton.AddToClassList(RemoveButtonUSSClassName);

			foldoutHeader.Add(_textField);
			foldoutHeader.Add(addButton);
			foldoutHeader.Add(removeButton);

			SetValueWithoutNotify(value);
		}

		public string GlobalId { get; set; }

		public string value
		{
			get => SmartIdTable.Utils.ConvertGlobalPathToLocal(GlobalId);
			set
			{
				GlobalId = Utils.UpdateGlobalPath(GlobalId, value);
				_foldout.viewDataKey = GlobalId;
				SetValueWithoutNotify(value);
			}
		}

		public override VisualElement contentContainer => _foldout.contentContainer;

		public void SetValueWithoutNotify(string newValue)
		{
			string displayValue = SmartIdTable.Utils.ConvertGlobalPathToLocal(newValue);
			// Debug.Log($"Full path: {newValue}; Display: {displayValue}");
			_textField.SetValueWithoutNotify(displayValue);
		}

		private void OnChildCountChanged()
		{
			bool hasChildren = contentContainer.childCount > 0;
			if (hasChildren)
				_checkmark.RemoveFromClassList(CLASS_DISABLED);
			else
				_checkmark.AddToClassList(CLASS_DISABLED);
		}

		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			private UxmlStringAttributeDescription _path;

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);

				if (ve is not IdDefinitionView idView)
					return;

				string path = _path.GetValueFromBag(bag, cc);
				idView.GlobalId = path;
				idView.SetValueWithoutNotify(path);
			}

			public UxmlTraits()
			{
				var attributeDescription1 = new UxmlStringAttributeDescription
				{
					name = "path"
				};

				_path = attributeDescription1;
			}
		}
	}
}