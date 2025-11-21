using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppFreelanceMediaApi.Domain.Models;

namespace WebAppFreelanceMediaApi.Data.Entities
{
    public class Ad
    {
        public int AdId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category {  get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public int StaffId {  get; set; }
        public Staff Staff { get; set; }

        public Ad FromModel(AdModel adModel)
        {
            if (adModel != null)
            {
                this.AdId = adModel.AdId;
                this.Title = adModel.Title;
                this.Description = adModel.Description;
                this.Category = adModel.Category;
                this.ImagePath = adModel.ImagePath;
                this.StaffId= adModel.StaffId;
            }
            return this;
        }

        public AdModel ToModel()
        {
            return new AdModel
            {
                AdId = this.AdId,
                Title = this.Title,
                Description = this.Description,
                Category = this.Category,
                ImagePath = this.ImagePath,
                StaffId = this.StaffId
            };
        }
    }
}
