using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace KiemTra.Models
{
    public class SinhVien
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã sinh viên không được vượt quá 50 ký tự")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public bool GioiTinh { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình ảnh không được vượt quá 255 ký tự")]
        public string? Hinh { get; set; }

        [Required(ErrorMessage = "Mã ngành là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã ngành không được vượt quá 50 ký tự")]
        public string MaNganh { get; set; }

        [ForeignKey("MaNganh")]
        public virtual NganhHoc? NganhHoc { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

