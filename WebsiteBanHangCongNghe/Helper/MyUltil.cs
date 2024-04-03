using System.Text;

namespace WebsiteBanHangCongNghe.Helper
{
	public class MyUltil
	{
		public static string GenerateRamdomKey(int length = 5)
		{
			var pattern = @"qazwsxedcrfvtgbyhnujmiklopQAZWSXEDCRFVTGBYHNUJMIKLOP!";
			var sb = new StringBuilder();
			var rd = new Random();
			for (int i = 0; i < length; i++)
			{
				sb.Append(pattern[rd.Next(0, pattern.Length)]);
			}

			return sb.ToString();
		}
        public static string SaveImage(IFormFile imgage, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imgs","Images", folder, imgage.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    imgage.CopyTo(myfile);
                }
                return imgage.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
