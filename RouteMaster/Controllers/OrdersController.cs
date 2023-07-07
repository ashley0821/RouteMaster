using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.DapperRepositories;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Odbc;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using RouteMaster.Models.Infra;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Xml.Linq;

namespace RouteMaster.Controllers
{
	public class OrdersController : Controller

	{
		private readonly AppDbContext db = new AppDbContext();
		//GET: Orders
		public ActionResult Index(OrderCriteria criteria)
		{
			ViewBag.Criteria = criteria;


			PreparePaymentStatusDataSource(null);
			PrepareMemberNameDataSource(null);
			IEnumerable<OrderIndexVM> orders = (IEnumerable<OrderIndexVM>)GetOrders(criteria);

			return View(orders);


		}
		[HttpPost]
		//Order 
		private IEnumerable<OrderIndexVM> GetOrders(OrderCriteria criteria)
		{
			IOrderRepository repo = new OrderEFRepository();
			OrderService service = new OrderService(repo);

			return service.Search(criteria)
				   .ToList()
				   .Select(o => o.ToIndexVM());
		}


		
		public ActionResult Details(int? id)
		{

			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order.ToIndexDto().ToIndexVM());
		}





		[HttpPost]
		public ActionResult Details(OrderIndexVM vm)
		{
			IOrderRepository repo = new OrderEFRepository();
			OrderService service = new OrderService(repo);
			db.Orders.Find(vm.Id).Total=vm.Total;
			db.SaveChanges();
			return RedirectToAction("Index");	
			
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", order.MemberId);
			ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "Id", "Name", order.PaymentMethodId);
			ViewBag.TravelPlanId = new SelectList(db.TravelPlans, "Id", "Id", order.TravelPlanId);
			return View(order);
		}

		// POST: test/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(OrderEditVM vm)
		{
			IOrderRepository repo = new OrderEFRepository();
			OrderService service = new OrderService(repo);

			if (ModelState.IsValid)
			{
				service.Edit(vm.ToEditDto());
				return RedirectToAction("Index");
			}

			//ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", order.MemberId);
			//ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "Id", "Name", order.PaymentMethodId);
			//ViewBag.TravelPlanId = new SelectList(db.TravelPlans, "Id", "Id", order.TravelPlanId);
			return View(vm);
		}


		//activitiesdetails(ef)

		//public actionresult indexdapper(int id)
		//{

		//	var viewmodelitems = db.activitiesdetails
		//		.tolist()
		//		.select(dto => new activitiesdetailsindexvm
		//		{
		//			id = dto.id,
		//			orderid = dto.orderid,
		//			activityid = dto.activityid,
		//			activityname = dto.activityname,
		//			starttime = dto.starttime,
		//			endtime = dto.endtime,
		//			price = dto.price,
		//			quantity = dto.quantity,

		//		});

		//	//viewmodelitems.select(x => x.orderid = id).tolist();
		//	return partialview("_indexdapper", viewmodelitems.where(x => x.orderid == id).tolist());
		//}


		//
		public ActionResult IndexDapper(int orderId)

		{
			ActivitiesDetailsDapperRepository repo = new ActivitiesDetailsDapperRepository();
			List<ActivitiesDetailsIndexVM> activitiesdetails = repo.GetActivitiesDetails(orderId);
			return PartialView("_IndexDapper", activitiesdetails);

		}
		public ActionResult ActivitiesDetailsEdit(int id)
		{
			IActivitiesDetailsRepository repo = new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service = new ActivitiesDetailsService(repo);

			ActivitiesDetailsEditVM editVM = service.GetActivitiesDetailsEditDetails(id);

			if (editVM == null)
			{
				return HttpNotFound();
			}
			return View(editVM);

		}
		[HttpPost]
		public ActionResult ActivitiesDetailsEdit(ActivitiesDetailsEditVM editvm)
		{
			IActivitiesDetailsRepository repo = new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service = new ActivitiesDetailsService(repo);

			if (ModelState.IsValid)
			{
				service.ActivitiesDetailsEdit(editvm.ToEditDto());
				return RedirectToAction("Details", new { id = editvm.OrderId }); 
			}
			return View(editvm);
		}

		public ActionResult ActivitiesDetailsDelete(int id)
		{
			IActivitiesDetailsRepository repo = new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service = new ActivitiesDetailsService(repo);

			var activitiesDetails = service.GetActivitiesDetailsById(id);
			return View(activitiesDetails.ToIndexDto().ToIndexVM());
		}

		[HttpPost, ActionName("ActivitiesDetailsDelete")]
		[ValidateAntiForgeryToken]
		public ActionResult ActivitiesDetailsDeleteConfirm(int id)
		{
			IActivitiesDetailsRepository repo = new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service = new ActivitiesDetailsService(repo);

			service.ActivitiesDetailsDelete(id);
			return RedirectToAction("Index");
		}

		//public ActionResult ActivitiesDetailsUpdate(int activitiesDetailsId, int newprice)
		//{
		//	var activitiesDetails = db.ActivitiesDetails.Find(activitiesDetailsId);
		//	activitiesDetails.Price= newprice;

		//	var order=db.Orders.FirstOrDefault(o=>o.Id== activitiesDetails.OrderId);
		//	if (order != null)
		//	{
		//		// 重新計算 Order 的金額，例如總金額為各個 ActivitiesDetails 的金額總和
		//		int total = db.ActivitiesDetails.Where(ad => ad.OrderId == order.Id).Sum(ad => ad.Price);
		//		order.Total = total;
		//		db.SaveChanges();
		//	}

		//	return RedirectToAction("Index");
		//}
		//ExtraServiceDetails (EF)
		//      public ActionResult ExtraServicesDetailsPartialView(int id)
		//{


		//	var viewModelItems = db.ExtraServicesDetails
		//						.ToList()
		//						.Select(dto => new ExtraServicesDetailsVM
		//						{
		//							Id = dto.Id,
		//							OrderId = dto.OrderId,
		//							ExtraServiceId = dto.ExtraServiceId,
		//							ExtraServiceName = dto.ExtraServiceName,
		//							Price = dto.Price,
		//							Quantity = dto.Quantity,
		//						});

		//	//viewModelItems.Select(x=>x.OrderId = id).ToList();
		//	//return PartialView("ExtraServicesDetailsPartialView",viewModelItems.Where(x=>x.OrderId==id).ToList());
		//	return PartialView("_ExtraServicesDetailsPartialView", viewModelItems.Where(x => x.OrderId == id).ToList());

		//}

		//AccomodationDetails (Dapper)



		public ActionResult ExtraServicesDetailsPartialView(int orderId)
		{
			ExtraServicesDetailsDapperRepository repo = new ExtraServicesDetailsDapperRepository();
			List<ExtraServicesDetailsVM> extraServicesDetails = repo.GetExtraServicesDetails(orderId);

			return PartialView("_ExtraServicesDetailsPartialView", extraServicesDetails);
		}

		public ActionResult ExtraServicesDetailsEdit(int id)
		{
			IExtraServiceDetailsRepository repo = new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service = new ExtraServicesDetailsService(repo);


			ExtraServicesDetailsEditVM editVM = service.GetExtraServicesEditDetails(id);

			if (editVM == null)
			{
				return HttpNotFound();
			}


			return View(editVM);
		}

		[HttpPost]

		public ActionResult ExtraServicesDetailsEdit(ExtraServicesDetailsEditVM editvm)
		{
			IExtraServiceDetailsRepository repo = new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service = new ExtraServicesDetailsService(repo);

			if (ModelState.IsValid)
			{
				service.ExtraServicesDetailsEdit(editvm.ToEditDto());
				return RedirectToAction("Details", new { id = editvm.OrderId });
			}

			return View(editvm);
		}

		public ActionResult ExtraServicesDetailsDelete(int id)
		{
			IExtraServiceDetailsRepository repo = new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service = new ExtraServicesDetailsService(repo);

			var extraServicesDetails = service.GetExtraServicesDetailsById(id);
			return View(extraServicesDetails.ToIndexDto().ToIndexVm());
		}

		[HttpPost, ActionName("ExtraServicesDetailsDelete")]
		[ValidateAntiForgeryToken]
		public ActionResult ExtraServicesDetailsDeleteConfirm(int id)
		{
			IExtraServiceDetailsRepository repo = new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service = new ExtraServicesDetailsService(repo);

			service.ExtraServicesDetailsDelete(id);
			return RedirectToAction("index");
		}



		public ActionResult AccomodationDetailsPartialView(int orderId)
		{
			AccomodationDetailsDapperRepository repo = new AccomodationDetailsDapperRepository();
			List<AccomodationDetailsVM> accomodationDetails = repo.GetAccomodationDetails(orderId);

			return PartialView("_AccomodationDetailsPartialView", accomodationDetails);
		}

		public ActionResult AccomodationDetailsEdit(int id)
		{
			IAccomodationDetailsRepository repo = new AccomodationDetailsDapperRepository();
			AccomodationDetailsService service = new AccomodationDetailsService(repo);

			AccomodationDetailsEditVM editVM = service.GetAccomodationDetailsEditDetails(id);
			if (editVM == null)
			{
				return HttpNotFound();
			}
			return View(editVM);

		}

		[HttpPost]
		public ActionResult AccomodationDetailsEdit(AccomodationDetailsEditVM editVM)
		{
			IAccomodationDetailsRepository repo = new AccomodationDetailsDapperRepository();
			AccomodationDetailsService service = new AccomodationDetailsService(repo);
			if (ModelState.IsValid)
			{
				service.AccomodationDetailsEdit(editVM.ToEditDto());
				return RedirectToAction("Details", new { id = editVM.OrderId });
			}
			return View(editVM);
		}
		//public ActionResult UpdateOrderTotal(Order dto)
		//{
		//	using (var conn = new SqlConnection(_connStr))
		//	{
		//		int activitiesTotal = OrderHelper.GetActivitiesTotal(conn, dto.OrderId);
		//		int extraServiceTotal = OrderHelper.GetExtraServiceTotal(conn, dto.OrderId);

		//		int total = activitiesTotal + extraServiceTotal;

		//		string sqlOrder = @"UPDATE Orders SET Total = @Total WHERE Id = @OrderId";
		//		conn.Execute(sqlOrder, new { Total = total, OrderId = dto.OrderId });
		//		return Json(sqlOrder);
		//		// 其他操作或返回結果
		//	}
		//}
		public ActionResult AccomodationDetailsDelete(int id)
		{
			IAccomodationDetailsRepository repo = new AccomodationDetailsDapperRepository();
			AccomodationDetailsService service = new AccomodationDetailsService(repo);

			var accomodationdetail = service.GetAccomodationDetailsById(id);
			return View(accomodationdetail.ToIndexDto().ToIndexVM());
		}
		[HttpPost, ActionName("AccomodationDetailsDelete")]
		[ValidateAntiForgeryToken]
		public ActionResult AccomodationDetailsDeleteConfirmed(int id)
		{
			IAccomodationDetailsRepository repo = new AccomodationDetailsDapperRepository();
			AccomodationDetailsService service = new AccomodationDetailsService(repo);
			service.AccomodationDetailsDelete(id);

			return RedirectToAction("Index");
		}
		private void PreparePaymentStatusDataSource(int? PaymentStatus)
		{

			IEnumerable<SelectListItem> paymentStatusList = new List<SelectListItem>
			{
				new SelectListItem { Value = "0", Text = "" },
		new SelectListItem { Value = "1", Text = "已付款" },
		new SelectListItem { Value = "2", Text = "未付款" },
		new SelectListItem { Value = "3", Text = "已取消" },
			};
			ViewBag.paymentStatus = new SelectList(paymentStatusList, "Value", "Text", PaymentStatus);
		}
		private void PrepareMemberNameDataSource(int? MemberId)
		{
			var member = db.Orders.ToList().Prepend(new Order());
			ViewBag.MemberId = new SelectList(member, "Id", "Name", MemberId);
		}


		private bool OrderExists(int id)
		{
			return (db.Orders?.Any(o => o.Id == id)).GetValueOrDefault();
		}
		[HttpPost]
		public JsonResult SendUnpaidNotification(List<int> selectedOrderIds)
		{
			EmailHelper emailHelper = new EmailHelper();

			foreach (var orderId in selectedOrderIds)
			{
				var order = db.Orders.Find(orderId);

				if (order != null)
				{
					var name = order.Member.FirstName;
					var email = order.Member.Email;

					emailHelper.SendUppaidNotification(name, email);
				}

			}
			return Json(new { success = true });
		}
		[HttpPost]
		public JsonResult UpdatePaymentStatus(int id, int PaymentStatus)
		{
			try
			{
				var order = db.Orders.Find(id);
				order.PaymentStatus = PaymentStatus;
				db.SaveChanges();
				return Json(new { success = true });
			}
			catch(Exception ex) 
			{
				return Json(new { success = false, errorMessage = ex.Message });
			}



		}

	
	}


}

       


		
