using WebsiteBanHangCongNghe.Data;

namespace WebsiteBanHangCongNghe.ViewModel
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string image { get; set; }
		public double price { get; set; }
		public string description { get; set; }

	}
	public class ProductDetailVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string image { get; set; }
		public double price { get; set; }
		public string description { get; set; }

		public string CategoryName { get; set; }
		public string BrandName { get; set; }
		public int? Rating { get; set; }
		public string InstockName { get; set; }
		public List<Comment> Comments { get; set; } // Danh sách các comment
		public Comment NewComment { get; set; } // Đối tượng để thêm comment mới

	}

}
