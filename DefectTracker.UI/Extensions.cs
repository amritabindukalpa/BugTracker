using DefectTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DefectTracker.UI
{
    public static class Extensions
    {
        public static IEnumerable<SelectListItem> ToDropDownList(this IEnumerable<Users> modelList, int? id)
        {
            return modelList.OrderBy(i => i.Name).Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = i.Id == id
            });
        }

        public static IEnumerable<SelectListItem> ToDropDownList(this IEnumerable<DefectStatus> modelList, int? id)
        {
            return modelList.OrderBy(i => i.Status).Select(i => new SelectListItem
            {
                Text = i.Status,
                Value = i.Id.ToString(),
                Selected = i.Id == id
            });
        }
    }
}