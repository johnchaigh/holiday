using System;
using Microsoft.AspNetCore.Identity;

namespace holiday.Data
{
	public class ApplicationUser : IdentityUser
	{
		public string firstName { get; set; }
		public string lastName { get; set; } 
		public int allocation { get; set; }
		public string? lineManager { get; set; }
	}
}