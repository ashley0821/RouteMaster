using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RouteMaster.Models.Interfaces
{
	public interface IAttractionRepository
	{
		IEnumerable<AttractionIndexDto> Search();

		bool ExistAttraction(string Name);

		void Create(AttractionCreateDto dto, HttpPostedFileBase[] files, String path);

		AttractionDetailDto Get(int id);

		IEnumerable<AttractionImageIndexDto> GetImages(int id);

		AttractionEditDto GetEditDto (int id);

		void Edit(AttractionEditDto dto);

		void Delete(int id);

		void EditImage(AttractionImageIndexDto dto, HttpPostedFileBase file, string path);

		void DeleteImage(int imageId);

		void UploadImage(AttractionImageIndexDto dto, HttpPostedFileBase[] files, String path);
	}
}
