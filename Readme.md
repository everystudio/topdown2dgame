# トップダウンのゲームを作る手順などをまとめる

# リソースの準備など

- 9:00

リポジトリの準備  
リソースを入手  
https://www.shoeisha.co.jp/book/download/3609/read  


# タイルマップでゲーム画面作成

タイルマップとは  
スプライト＞タイルセット＞タイルパレット＞タイルマップという流れ  

1つの画像から複数のタイルを用意する  
TileMapという画像からタイルを切り出す設定を行う  
画像のインスペクターで以下設定 -------------------  
SpriteMode > Multiple  
Pixel Per Unit : 32  

Advanced  
 Filter Mode > Point(no filter)  
  ↑ドット絵の縁がきれいに表示される  
-------------------------------------------
設定できたらApplyで適応  

## SpriteEditorで編集を行う
Slice  
Grid By Cell Size  
32x32  
ほかは０でOK  
スライスがうまくできたらApply  

# タイルアセットを作る  
Window>2D>TilePalette  
CreateNewPalette  

Assets/Mapフォルダを作成してその中にStageMapというパレットを作る  
先程スライスした画像もMapフォルダへ移動させる。  

画像をパレットに放り込むとGenerate tiles into folderというウインドが出てくる  
Mapフォルダを選択するとその中にタイルが作られます  

- 10:00  

# タイルマップで実際にマップを作成する

H+  
2D Object>Tilemap(Rectanglar)  
ワールドマップとダンジョン１を作る  
別々のシーンに作成  

## タイルマップに当たり判定をつける

TilemapにAddComponent Tilemap>TilemapCollider2Dを追加  
これだとタイルマップ全体に当たり判定が発生するため、タイル側に設定を入れる  
床系のタイルアセットを選択して、ColliderType : Noneに変更

# プレイヤーキャラ作成

Assets/Playerフォルダを作成  
PlayerImageをPlayerフォルダへ移動  
PixelPerUnit 32  
SpriteMode Multiple  
Point no filder  

Slice CellSize  
32x32  
Pivot Customにして(0.5,0.2)にセット  
さらに弓は手で持つ場所にPivot変更  
矢は中心

## プレイヤーオブジェクト作成

PlayerImageのスプライト、PlayerImage_0,PlayerImage_1を選択してシーンビューにドラッグアンドドロップ  
作成されるアニメーションの名前をPlayerDownとしてPlayerフォルダに保存。  
SpriteRenderer Order in layer 3に設定  
オブジェクトの名前をPlayer , アニメーションコントローラーをPlayerAnimeに変更  

11:00

AddComponent  
+RigidBody2D(Gravity0 , FreezeRotationZ)
+CapsuleCollider2D(大きさ合わせるoff(0.0,0.2)size(0.6,0.8))

アニメーションが早すぎるのでサンプリングレートを変更する  
2020とかだとデフォルトで設定変更できないので、その場合はAnimationビューの右側の縦に３つ点が並んでいるところから
サンプリングレートを表示して変更する  
変更する値は4

PlayerUp,PlayerLeft,PlayerRightも同様の手順で作成  
オブジェクトは不要なのでアニメーションを作成後は消してOK

アニメーションコントローラー(PlayerAnime)からAnimatorビューを表示する  
PlayerDownのみのはずなので、先程作ったUp/Left/Rightをドラッグアンドドロップで追加する

ゲームオーバー用のアニメーションも追加したい。

AnimationビューからCraeateNewClipでPlayerDeadを作る
スプライトは9を使う

プレイヤーのスクリプトを作成

13:00

# 弓と矢のオブジェクトを用意する

弓と矢の画像をシーンにドラッグアンドドロップ

弓はBowに名前を変更  
座標を0,0にしてプレファブ化。シーン内のBowは削除  
プレファブはPlayerフォルダに入れておく  

矢は名前をArrowに変更  
レイヤーとタグをArrowに変更（追加してな）  
RigidBody2d/CircleCollider2Dをアタッチ  
RigidBody2d GravityScale:0  
サークルコライダー２ｄは矢の先端部分の当たり判定にセットする

弓のスクリプト作成  
プレイヤーにアタッチ、Bow,Arrowのプレファブをインスペクターにセットする  

矢のスクリプト作成  
レイヤーのマトリックスでPlayerとArrowを当たらないようにしておく
（プレイヤーのレイヤーをPlayerにしておく）

矢を打つところまで完了

カメラの追従は、Cinemachine使いましょ

あとは敵(Enemyというタグ)と当たったらダメージを受ける演出を追加

14:30
一旦作業終了

21:30再開

# シーンからシーンへ移動する

Exitというタグを作る  

スクリプト作成
- Exit  
- RoomManager

プレイヤーもPlayerというタグをセットしてないと駄目だった。  

Exitは触れるとシーンを移動するタイプのもの。
SceneNameに移動先のシーン名、DoorNumberには対応したドアの番号を追加。
向きは設置しているExitの向きでOK

RoomManagerは各シーンに１つ設置


# ドアを設置する

Tag:Doorを追加  
ドアの画像のPixelPerUnit : 32  
スクリプト作成  

ドアをプレファブ化  
スプライト配置、OrderInLayer2。タグをDoorに変更  
BoxCollider2Dをつける  

とりあえず鍵を持っていたら


# アイテムを作る

Itemsの画像のスプライトを準備32x32系  
鍵、ハート、矢の束の画像をシーン内にドラッグアンドドロップ  

CircleCollider2D + Rigidbody2D  
GravityScale=0  

CircleCollider2D  
IsTrigger true;  

Item用のスクリプト作成  

鍵：扉を開けるようのarrangeIDの設定が必要になる（配置時）  
矢：countの数だけ発射できる本数が増える  
ライフ：特になし！  

# 宝箱を作る

画像セット、order in layer 2  
capsulecollider2d追加  
マルでもいいよ  

スクリプト作成

インスペクターには開いた時の画像をセットしましょう

# 敵の作成

画像の設定

倒れてる画像をシーンビューにセット  
+RigidBody2D  
- GravityScale=0
- Freeze RotationZ ON
+CircleCollider2D  
SpriteRenderer-OrderInLayer=3

敵のスクリプトを作成

# UI作成

- 鍵、矢の本数表示
- ライフの表示
- ゲームのステータス

# VirtualPadの作成

なんか本のやつ気に入らなかったので作り直した。
というかたまたまヒエラルキーの構成でうまく出来てるだけなので、もう少し作り直したほうが良いと思う








