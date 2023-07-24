# ColorSettings
Color Settings는 협업을 위해 만들어진 기능입니다.

## 개발 목적
개발자가 직접 Color을 지정할 수도 있지만, 이해관계자들(디자이너 및 아티스트, 기획자 등)이 접근하여 수정할 수 있는 방법에 대해 고민을 해보았으며, 
코드를 접근하지 않고 최대한 Unity기능 상에서 수정가능하도록 하기위해 개발하게 되었습니다.

## 기능 설명
Resources 폴더의 ScriptableObject인 ColorSettings에 접근하여, 이해관계자들이 Inspector에서 해당 Color를 지정하거나 Material을 지정할 수 있습니다.  
또한 개발자는 해당 Color와 Material을 Enum값을 통해서 어디서든 접근할 수 있도록 [ColorSettings]의 [Instance]로 접근가능하도록 구현하였습니다.  

## 개발 후 단점에 대한 고찰
해당 ColorSettings은 뚜렷한 단점이 존재한다.
  
**"Resources 폴더에 ColorSettings ScriptableObject가 없을 경우, 에러를 발생시킨다는 점"**  
**"해당 기능을 모르는 인원이 ScriptableObject 자체를 삭제할 수 있는 경우가 발생한다는 점"**
