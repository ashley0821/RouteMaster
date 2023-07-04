
7/3 (上傳前更正回來-- RouteMaster.Models.Infra.Extensions的 public static RoomCreateDto ToDto(this RoomCreateVM vm)補回去Id = vm.Id,)
[]確認各功能運作 
[]Register的上傳圖片 //大頭貼的require有驗證問題
	1.上傳圖片
	2.儲存檔名
	3.回存到db 同時也要存到另一個欄位，
[]MemberImage/Name 改成允許null

----------
7/2接續做memberactive的功能
[]帳號停權的商業邏輯
[]再layout那使用權限來做登入才能顯示的畫面
----------
7/1
[√]建造Login ViewPage
[]啟用帳戶功能
[]忘記密碼
----------
6/30 
[√]建立 Member-RegisterPage
[√]連結到RegisterPage
[√]建立 Confirmpage
[√]密碼雜湊

*********************************
功能列表
1.Register
[]啟用帳戶--詢問組長技術長!!! 
[]成功註冊會員
[]註冊時新增圖片
[]修改圖片與修改會員資料分開
[]密碼雜湊步驟---此步驟不應該在viewModel或是Repository，應該在service層!
 

2.Edit 06120049 []MemberImage是相片庫
[]修改會員資料
[]修改圖片


3.Delete(刪除功能變更為停權功能)
[]停權功能-1.把密碼改成雜湊，以及變更為未開通


4.Search
[]Index頁搜尋


5.登入/登出/忘記密碼
[]登入
[]登入-登入三次錯誤
[]登入-機器人驗證
[]登出
[]忘記密碼


6.權限管理的crud