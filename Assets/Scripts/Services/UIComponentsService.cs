using UnityEngine;
using UnityEngine.UIElements;

public interface IUIComponentsService
{
  Button GetButton(string buttonName);
  Label GetLabel(string labelName);
}

public class UIComponentsService : IUIComponentsService
{
  private readonly VisualElement _uiRoot;
  private readonly UIDocument _ui;

  public UIComponentsService(UIDocument ui)
  {
    _ui = ui;
    _uiRoot = ui.rootVisualElement;
  }

  public Button GetButton(string buttonName)
  {
    return _uiRoot.Q<Button>($"{buttonName}");
  }

  public Label GetLabel(string labelName)
  {
    return _uiRoot.Q<Label>($"{labelName}");
  }


}
