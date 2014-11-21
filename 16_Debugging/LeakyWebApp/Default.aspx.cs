using System;
using System.Web;

namespace LeakyWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        private int[] bigData = new int[1000000];

        protected void Page_Load(object sender, EventArgs e)
        {
            InterestingStuffIsHere.Instance.SomethingInterestingForPages += Global_SomethingInterestingForPages;
        }

		public override void Dispose()
		{
			InterestingStuffIsHere.Instance.SomethingInterestingForPages -= Global_SomethingInterestingForPages;
			base.Dispose();
		}

        void Global_SomethingInterestingForPages(object sender, EventArgs e)
        {
        }
    }
}
