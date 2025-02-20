# 卒研で開発したMRシステムのプロジェクトファイルを公開しています。
対応Unity Verison : 2022.3.46f1\
使用しているMeta XR All-in-One SDK Verison : 71.0.0\
動作確認済みHMD : Meta Quest 3

## 使用方法
### 1. プロジェクトファイルをクローンする
```
git clone https://github.com/sleetind/portal_Quest3_XRSDKOnly.git
```

### 2.Unity 2022.3.46f1をUnity Hubからインストール
詳細は卒論付録Aに記述されています

### 3.Add Projectからcloneしたファイルを選択し、プロジェクトを開く
SDKのファイルが大きいので時間がかかります。プロジェクトが開かれたらEditorの再起動を促されるので再起動してください。

### 4.Project Setup Tool設定
Editorのhierarchy上あたりに「Meta XR Tools」という項目があるので、Meta XR Tools > Project Setup Tool とクリック\
Androidのアイコン(Project Setup Tools右側)を選択するとChecklistにIssuesとRecommended Itemsとあるので、それぞれ「Fix All」「Apply All」をクリック\
※IssuesやRecommended Itemsは残る場合がありますが、特に問題ないです。

### 4.Build Settings設定
メニューバーのEdit > Build Settings と選択\
Windows、Mac、Linuxになってると思うので、AndroidにSwitch Platformしてください(少し時間がかかります)

### Meta Quest 3にデプロイ
PCとMeta Quest 3を接続してBuild and Runでデプロイ\
(一度.apkファイルを作成したら次からはMeta Quest Developer Hubからもデプロイが可能です)
