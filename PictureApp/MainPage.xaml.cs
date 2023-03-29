using static Android.Graphics.ImageDecoder;
namespace PictureApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void BtnPickPhoto_Clicked(object sender, EventArgs e)
    {
        try
        {
            var file = await MediaPicker.Default.PickPhotoAsync().ConfigureAwait(true);

            if (file == null)
                return;

            Stream stream = await file.OpenReadAsync();

            this.imgShowImage.Source = ImageSource.FromStream(() => stream);
            this.lblFilename.Text = $"Filename: {file.FileName}";
        }
        catch // (Exception ex)
        {
            await DisplayAlert("Uh oh", "Something went wrong, but don't worry. It's software", "OK");
        }
    }

    private async void BtnTakePhoto_Clicked(object sender, EventArgs e, Stream sourceStream)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                Stream stream = await photo.OpenReadAsync();
#if WINDOWS
             var context = Platform.CurrentActivity;
#elif IOS || MACCATALYST                   
                //do something
#endif
            }
        }
    }
}