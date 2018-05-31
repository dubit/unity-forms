using System;
using UnityEngine;

namespace DUCK.Forms.Fields
{
	public class RadioField : AbstractFormField
	{
		[SerializeField]
		private RadioButton[] radioButtons;
		[SerializeField]
		private bool hasDefaultValue;
		[SerializeField]
		private int defaultValue;

		private RadioButton selectedRadioButton;

		protected override void Awake()
		{
			base.Awake();

			if (radioButtons.Length == 0)
			{
				throw new Exception("Radio field needs to have atleast one radio button.");
			}

			foreach (var radioButton in radioButtons)
			{
				var button = radioButton;
				button.SetSelected(false);
				button.OnSelected += () => SelectRadio(button);
			}

			SetDefaultValue();
		}

		public override object GetValue()
		{
			return selectedRadioButton ? selectedRadioButton.Id : null;
		}

		private void SelectRadio(RadioButton selected)
		{
			foreach (var radioButton in radioButtons)
			{
				radioButton.SetSelected(radioButton == selected);
			}

			selectedRadioButton = selected;
		}

		protected override void SetDefaultValue()
		{
			if (hasDefaultValue)
			{
				SelectRadio(radioButtons[defaultValue]);
			}
			else
			{
				foreach (var radioButton in radioButtons)
				{
					radioButton.SetSelected(false);
				}
			}
		}
	}
}