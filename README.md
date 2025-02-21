# 卒研で開発したMRシステムのプロジェクトファイルを公開しています。
対応Unity Verison : 2022.3.46f1\
使用しているMeta XR All-in-One SDK Verison : 71.0.0\
動作可能HMD : Meta Quest 3
## 使用方法
### 1. プロジェクトファイルをクローンする
```
git clone https://github.com/sleetind/portal_Quest3_XRSDKOnly.git
```

### 2.Unity 2022.3.46f1をUnity Hubからインストール
詳細は卒論付録Aに記述されています

### 3.Unity Hub > Add Projectからcloneしたファイルを選択し、プロジェクトを開く
SDKのファイルが大きいので時間がかかります。プロジェクトが開かれたらEditorの再起動を促されるので再起動してください。

### 4.Project Setup Tool設定
Editorのhierarchy上あたりに「Meta XR Tools」という項目があるので、Meta XR Tools > Project Setup Tool とクリック\
Androidのアイコン(Project Setup Tools右側)を選択するとChecklistにIssuesとRecommended Itemsとあるので、それぞれ「Fix All」「Apply All」をクリック\
※IssuesやRecommended Itemsは残る場合がありますが、特に問題ないです。動かない場合はIssuesにお願いします。(Quest  3がしばらく手元にない状態となるため、修正対応がいつになるかは不明です。)

### 4.Build Settings設定
メニューバーのEdit > Build Settings と選択\
Windows、Mac、Linuxという項目が選択されていると思うので、AndroidにSwitch Platformしてください(少し時間がかかります)

### Meta Quest 3にデプロイ
Build SettingsにあるScene in BuildにPortal Scene、Occlusion Scene、Controller Haptics Sceneがあることを確認してください。\
追加した状態で公開していますが、もしなかった場合はBuild Settingsを開いた状態でEditor上からAssets > Sceneと選択し、中にあるシーンをすべてScene in Buildにドラッグアンドドロップしてください。
PCとMeta Quest 3を接続してBuild and Runでデプロイ\
(一度.apkファイルを作成したら次からはMeta Quest Developer Hubからもデプロイが可能です)
