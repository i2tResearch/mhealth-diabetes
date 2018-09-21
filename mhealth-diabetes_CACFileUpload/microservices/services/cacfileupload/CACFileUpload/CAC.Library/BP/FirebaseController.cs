using CAC.Library.Model.DTO;
using CAC.Library.Utilities;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CAC.Library.BP
{
    public class FirebaseController
    {
        private static string ApiKey = "";
        private static string Bucket = "";
        //private static string Database = "";
        private static string AuthEmail = "";
        private static string AuthPassword = "";

        public static int Progress { get; set; }

        public static async Task<string> WriteOnFirebaseStorage(byte[] file, DTOArchivo archivo)
        {
            var stream = new MemoryStream(file);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var cancellation = new CancellationTokenSource();
            string url = "";
            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child(archivo.IdUsuario)
                .Child(archivo.Id)
                .Child(archivo.Nombre)
                .PutAsync(stream, cancellation.Token);

            task.Progress.ProgressChanged += (s, e) => Progress = e.Percentage;
            try
            {
               url = await task;
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FirebaseController>());
            }
            return url;
        }

        //public static async void WriteOnFirebaseDatabase(byte[] file)
        //{

        //}
    }
}
