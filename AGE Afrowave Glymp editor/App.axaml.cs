using AGE_Afrowave_Glymp_editor;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AGE_Afrowave_Glyph_editor
{
   public partial class App : Application
   {
      public override void Initialize()
      {
         AvaloniaXamlLoader.Load(this);
      }

      public override void OnFrameworkInitializationCompleted()
      {
         if(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
         {
            desktop.MainWindow = new MainView();
         }

         base.OnFrameworkInitializationCompleted();
      }
   }
}