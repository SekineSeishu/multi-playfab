# PlayFab（制作途中)

インベントリの表示                       
PlayFabInventory(サーバーからプレイヤーのインベントリ情報を取得)//ショップならここがPlayFabshopに変わる <br>
↓<br>
Inventory（インベントリに表示するアイテムデータの受け渡し)   
↓<br>
InventoryUI（受け取ったアイテムデータを表示）<br>
この流れでUIに表示している<br>
<br>
PlayFabInventory　https://github.com/SekineSeishu/multi-playfab/blob/main/Assets/Script/Inventry/PlayFabInventry.cs<br>
PlayFabShop  https://github.com/SekineSeishu/multi-playfab/blob/main/Assets/Script/Shop/PlayfabShop.cs<br>
ゲーム内で実装されていないデータを所持していた場合UIにはデータを表示しないようにゲーム内に実装されている全てのデータリスト内に検索をかけて確認している<br>
PlayFabShopではプラスでインベントリに既に所持していないかを確認する<br>
<br>
InventoryUI  https://github.com/SekineSeishu/multi-playfab/blob/main/Assets/Script/Inventry/InventoryUI.cs<br>
アイテムデータを持つスロットオブジェクトを指定位置に生成後下の段に新しい指定位置を作る<br>
インベントリの最大容量の実装、最大容量の変化の実装を見越して半永久的にアイテムスロットを生成できるようにした<br>
<br>
LobbyManager  https://github.com/SekineSeishu/multi-playfab/blob/main/Assets/Script/Lobby/LobbyManager.cs<br>
※StartPrivateLobby()は現状はまだ使用していない
JoinPrivateLobby()で同じロビーコードのロビーがあったら他プレイヤーとマッチングする


