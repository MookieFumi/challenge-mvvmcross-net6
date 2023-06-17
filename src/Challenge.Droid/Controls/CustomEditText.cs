using System.Windows.Input;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace Challenge.UI.Droid.Controls
{
    public class CustomEditText : EditText
    {
        public CustomEditText(Context c, IAttributeSet a) : base(c, a)
        {
            this.EditorAction += EventHandler;
        }

        public ICommand SearchCommand { get; set; }

        public void EventHandler(object MyObject, EditText.EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == Android.Views.InputMethods.ImeAction.Search ||
                e.ActionId == Android.Views.InputMethods.ImeAction.Send ||
                e.ActionId == Android.Views.InputMethods.ImeAction.ImeNull ||
                e.ActionId == Android.Views.InputMethods.ImeAction.Go)
            {
                if (SearchCommand != null)
                {
                    SearchCommand.Execute(null);
                    e.Handled = true;
                }
            }
        }
    }
}

