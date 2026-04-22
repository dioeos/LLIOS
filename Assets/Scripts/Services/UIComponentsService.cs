using UnityEngine;
using UnityEngine.UIElements;

public interface IUIComponentsService
{
  Button GetButton(string buttonName);
  Label GetLabel(string labelName);
}

public class UIComponentsService : IUIComponentsService
{
  private readonly UIDocument _ui;

  public UIComponentsService(UIDocument ui)
  {
    _ui = ui;
  }

  public Button GetButton(string buttonName)
  {
    return _ui.rootVisualElement?.Q<Button>($"{buttonName}");
  }

  public Label GetLabel(string labelName)
  {
    return _ui.rootVisualElement?.Q<Label>($"{labelName}");
  }
}
