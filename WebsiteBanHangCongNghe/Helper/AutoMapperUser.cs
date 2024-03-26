using AutoMapper;
using WebsiteBanHangCongNghe.Data;
using WebsiteBanHangCongNghe.ViewModel;

namespace WebsiteBanHangCongNghe.Helper
{
	public class AutoMapperUser : Profile
	{
		public AutoMapperUser() {
			CreateMap<UserRegister, User>();
		}
	}
}
