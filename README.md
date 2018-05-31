# unity-forms

## What is it?
Forms are designed to make UserInterface forms quicker. A form can hold multiple fields with pre-made fields available such as:

* Text Input
* Radio button
* Dropdown
* Checkbox

However you can extend AbstractFormField.cs and make a custom field.
Also included are Validators, validators are components you can add to the field GameObject that determine if the form can be submitted successfully. Validators available are:

* Regex
* FieldMatch
* CheckBox IsOn
* NullOrEmpty
* Text length

With options like clear field on submit and the OnValidationFailed event you can customize the users experience and feedback.

## How to use it.
Create your fields and assign unique id's to each one.
Reference the fields in the form component.
Apply an extra validator components to the fields.
Assign the submit button to the Form component.

```c#
form.OnSubmitForm += HandleFormSubmitted;

private void HandleFormSubmitted(Dictionary<string, AbstractFormField> fields)
{
	var email = fields["email"].GetStringValue();
	var password = fields["password"].GetStringValue();
}
```
`OnSubmitForm` will only Invoke if all validators return valid.

## Examples
Examples are included in the Examples folder. 
It includes an example login and registration form with form field validation messages.