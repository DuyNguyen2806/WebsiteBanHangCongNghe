using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebsiteBanHangCongNghe.ViewModel
{
	public class LoginVM
	{
		[DisplayName("Tên đăng nhập")]
		[Required(ErrorMessage = ("*"))]

		public string Username { get; set; }

		[DisplayName("Mật khẩu")]
		[Required(ErrorMessage = ("*"))]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
