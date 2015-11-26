using System;
using System.Web;
using System.Web.UI.WebControls;

namespace LeakyWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        Button submitButton = new Button() {Text = "Save"};
        private int[] bigData = new int[1000000];

        protected void Page_Load(object sender, EventArgs e)
        {
            submitButton.Click += SubmitButtonOnClick;
            InterestingStuffIsHere.Instance.SomethingInterestingForPages += Global_SomethingInterestingForPages;
        }

        public override void Dispose()
        {
            InterestingStuffIsHere
                .Instance.SomethingInterestingForPages -=
                Global_SomethingInterestingForPages;

            base.Dispose();
        }

        private void SubmitButtonOnClick(object sender, EventArgs eventArgs)
        {
        }

        void Global_SomethingInterestingForPages(object sender, EventArgs e)
        {
        }
    }
}
