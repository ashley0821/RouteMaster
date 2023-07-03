
7/3 (上傳前更正回來-- RouteMaster.Models.Infra.Extensions的 public static RoomCreateDto ToDto(this RoomCreateVM vm)補回去Id = vm.Id,)
[]確認各功能運作 
[√]Register的上傳圖片 //大頭貼的require有驗證問題
	1.上傳圖片
	2.儲存檔名
	3.回存到db 同時也要存到另一個欄位，
[]MemberImage/Name 改成允許null -- 告知組長
[]加入jquery.ui到content資料夾，加jquery.js到Script資料夾裡面去
[]<link rel="stylesheet" href="~/Content//jquery-ui.css"> 加到layout

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
[]啟用帳戶--詢問組長技術長!!! 能做就做 
[√]成功註冊會員
[√]註冊時新增圖片
[]修改圖片與修改會員資料分開
[]密碼雜湊步驟---此步驟不應該在viewModel或是Repository，應該在service層!
 

2.Edit 06120049 []MemberImage是相片庫
[]修改會員資料
[]修改圖片


3.Delete(因不刪除會員，刪除功能變更為停權功能)
[]停權功能 -- 寫功能把會員IsSuspended變更為未開通


4.Search
[working on]Index頁搜尋 -- 剩下條件沒寫


5.登入/登出/忘記密碼
[√]登入
[]登入-登入三次錯誤
[]登入-機器人驗證
[]登出
[]忘記密碼


--viewpage版面配置

6.權限管理的crud
  1.屬性網址 -- https://learn.microsoft.com/zh-tw/dotnet/api/system.web.mvc.authorizeattribute?view=aspnet-mvc-5.2