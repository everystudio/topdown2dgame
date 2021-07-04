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



