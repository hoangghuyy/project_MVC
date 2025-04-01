using project_mvc.Services.Admin.Models;
using System;
using static project_mvc.Services.Admin.Models.Attributes;

namespace project_mvc.Areas.Admin.ViewModels
{
	public class AttributesViewModel
	{
		public List<AttributeItem>? ListItems { get; set; }
		public Attributes? Item { get; set; }

	}
}
