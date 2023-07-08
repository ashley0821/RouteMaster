0708 解決問題
[]Memberindex頁的萬年曆沒有箭頭
[]大頭貼預設圖片
[]parthnre搜尋功能
[]administrator搜尋功能

0707
[]實作一次所有功能
  1.[]login後臺管理者 -- 如果可以登入三次即鎖定
  2.[]設計自己帳號管理介面的權限再回去測試login
  
  []旅宿夥伴
    []啟用帳戶
    []忘記/更改密碼
    []Iindex搜尋
    []停權 -只有管理者可以
    []login
    []logout

  []後臺管理者
    []啟用帳戶
    []忘記/更改密碼
    []Index搜尋
    []停權 --只有帳戶管理者可以
    []login
    []logout

(0706 11:30 目前進度推估55%~60%)
7/6
[√]測試權限驗證
[]先完成AdministratorLogin完成
[]忘記密碼
[]啟用帳戶發送信件
[]註冊會員沒有萬年曆
----------
(0705 09:00 目前進度推估40%)
7/5
[√]研究權限研究太久，花費太多時間

----------
7/4
[]完成MemberCRUD
[√]edit 的萬年曆
[]edit轉三層式
[√]searchfunction 
[√]edit圖片存進member，但尚未存進MemberImage

----------
7/3 (上傳前更正回來-- RouteMaster.Models.Infra.Extensions的 public static RoomCreateDto ToDto(this RoomCreateVM vm)補回去Id = vm.Id,)
[]確認各功能運作 
[√]Register的上傳圖片 //大頭貼的require有驗證問題
    1.上傳圖片
    2.儲存檔名
    3.回存到db 同時也要存到另一個欄位，
[√]MemberImage/Name 改成允許null -- 告知組長
[√]加入jquery.ui到content資料夾，加jquery.js到Script資料夾裡面去
[√]<link rel="stylesheet" href="~/Content//jquery-ui.css"> 加到layout

----------
7/1
[√]建造Login ViewPage

----------
6/30 
[√]建立 Member-RegisterPage
[√]連結到RegisterPage
[√]建立 Confirmpage
[√]密碼雜湊

*********************************
ppt報告流程: 
   *AdministratorLogin 
    1.login
    2.忘記密碼
    3.登入權限錯誤

   *展示各大項功能的權限不足
    1.權限不足的頁面
   
   *會員管理
    1.搜尋
    2.排序
    3.停權管理
    4.註冊帳號 - 啟用帳號

   *AdministratorLogout
    
     

已完成功能

1.Register
  []啟用帳戶--詢問組長技術長!!! 能做就做  
  [√]成功註冊會員
  [√]註冊時新增圖片
 

2.Edit 06120049 []MemberImage是相片庫
  []修改會員資料
  []修改圖片


3.Delete(因不刪除會員，刪除功能變更為停權功能)
  []停權功能 -- 寫功能把會員IsSuspended變更為未開通


4.Search
  [√]Index頁搜尋 


5.登入/登出/忘記密碼
  [√]登入
  []登入-登入三次錯誤
  []登入-機器人驗證
  []登出
  []忘記密碼


--viewpage版面配置

6.權限管理
  1.屬性網址 -- https://learn.microsoft.com/zh-tw/dotnet/api/system.web.mvc.authorizeattribute?view=aspnet-mvc-5.2
  2.參考網址:
     *https://dotblogs.com.tw/ricochen/2010/03/19/14113
     *https://ithelp.ithome.com.tw/articles/10308230
     *https://dotblogs.com.tw/JesperLai/2018/03/20/170705
     *https://ithelp.ithome.com.tw/questions/10193367
     *https://exfast.me/2016/07/c-asp-net-mvc5-inherit-authorizeattribute-to-implement-custom-validation/

  3.Filter:
     *https://dotblogs.com.tw/Jamis/2016/01/09/125624
     *https://ithelp.ithome.com.tw/articles/10206966
     *https://ithelp.ithome.com.tw/articles/10198206

權限的作法:
1.先寫一個public class xxxxxx : AuthorizeAttribute <== 繼承
2.再覆寫裡面的 AuthorizeCore // protected override bool AuthorizeCore(HttpContextBase httpContext)


    **權限的分類:
      0707下午找時間跟組員討論