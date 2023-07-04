﻿using RouteMaster.Models.Dto;
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
using System.Data.Odbc;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
		//Order 
		private IEnumerable<OrderIndexVM> GetOrders(OrderCriteria criteria)
		{
			IOrderRepository repo = new OrderEFRepository();
			OrderService service = new OrderService(repo);

			return service.Search(criteria)
				   .ToList()
				   .Select(o => o.ToIndexVM());
		}
		

		public ActionResult Details(int id)
		{

			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order);
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

			ActivitiesDetailsEditVM editVM= service.GetActivitiesDetailsEditDetails(id);

			if (editVM == null)
			{
				return HttpNotFound();
			}
			return View("_ActivitiesDetailsEdit", editVM);

		}
		[HttpPost]
		public ActionResult ActivitiesDetailsEdit(ActivitiesDetailsEditVM editvm)
		{
			IActivitiesDetailsRepository repo = new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service= new ActivitiesDetailsService(repo);

			if (ModelState.IsValid)
			{
				service.ActivitiesDetailsEdit(editvm.ToEditDto());
				return RedirectToAction("Index");
			}
			return View(editvm);
		}

		public ActionResult ActivitiesDetailsDelete(int id)
		{
			IActivitiesDetailsRepository repo=new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service=new ActivitiesDetailsService(repo);

			var activitiesDetails = service.GetActivitiesDetailsById(id);
			return View(activitiesDetails.ToIndexDto().ToIndexVM());
		}

		[HttpPost, ActionName("ActivitiesDetailsDelete")]
		[ValidateAntiForgeryToken]
		public ActionResult ActivitiesDetailsDeleteConfirm(int id)
		{
			IActivitiesDetailsRepository repo=new ActivitiesDetailsDapperRepository();
			ActivitiesDetailsService service= new ActivitiesDetailsService(repo);

			service.ActivitiesDetailsDelete(id);
			return RedirectToAction("Index");
		}

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
			

			return View("_ExtraServicesDetailsEdit", editVM);
		}

		[HttpPost]

		public ActionResult ExtraServicesDetailsEdit(ExtraServicesDetailsEditVM editvm)
		{
			IExtraServiceDetailsRepository repo = new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service = new ExtraServicesDetailsService(repo);

			if (ModelState.IsValid)
			{
				service.ExtraServicesDetailsEdit(editvm.ToEditDto());
				return RedirectToAction("Index");
			}

			return View(editvm);
		}

		public ActionResult ExtraServicesDetailsDelete(int id)
		{
			IExtraServiceDetailsRepository repo=new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service=new ExtraServicesDetailsService(repo);

			var extraServicesDetails=service.GetExtraServicesDetailsById(id);
			return View(extraServicesDetails.ToIndexDto().ToIndexVm());
		}
		
		[HttpPost, ActionName("ExtraServicesDetailsDelete")]
		[ValidateAntiForgeryToken]
		public ActionResult ExtraServicesDetailsDeleteConfirm(int id)
		{
			IExtraServiceDetailsRepository repo=new ExtraServicesDetailsDapperRepository();
			ExtraServicesDetailsService service=new ExtraServicesDetailsService(repo);

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
			AccomodationDetailsService service=new AccomodationDetailsService(repo);

			AccomodationDetailsEditVM editVM = service.GetAccomodationDetailsEditDetails(id);
			if (editVM == null)
			{
				return HttpNotFound();
			}
			return View("_AccomodationDetailsEdit",editVM);

		}

		[HttpPost]
		public ActionResult AccomodationDetailsEdit(AccomodationDetailsEditVM editVM)
		{
			IAccomodationDetailsRepository repo = new AccomodationDetailsDapperRepository();
			AccomodationDetailsService service=new AccomodationDetailsService(repo);
			if (ModelState.IsValid)
			{
				service.AccomodationDetailsEdit(editVM.ToEditDto());
				return RedirectToAction("Index");
			}
			return View(editVM);
		}

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

		[HttpPost]
		public ActionResult GetOrders()
		{
			if (db.Orders == null)
			{
				return null;
			}
			//DbSet是紀錄的集合，他是可以列舉的，所以我們把ToList刪除掉

			IEnumerable<OrderIndexVM> data = db.Orders.Select(order => new OrderIndexVM
			{
				Id = order.Id,
				MemberName = order.Member.FirstName,
				PaymentMethodName = order.PaymentMethod.Name,
				CreateDate = order.CreateDate,
				Total = order.Total
			}).ToList();
			return Json(data, JsonRequestBehavior.AllowGet);

		}
		public ActionResult GetOrdersData()
		{
			var orders = GetOrders(); // 调用你的 GetOrders() 方法获取订单数据
			return Json(orders);
		}



	}
}

		
