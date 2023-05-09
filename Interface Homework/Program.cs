using System.Transactions;
using System.Xml.Schema;

namespace Backup
{

    static public class UfM
    {
        public static string? getShortGuid()
        {
            Guid guid = Guid.NewGuid();
            Console.WriteLine(guid);
            string shortguid = "";
            for (int i = 0; i < 8; i++)
            {
                shortguid += guid.ToString()[i];
            }
            return shortguid;
        }

    }

    public class MediaFile
    {
        public MediaFile(string? mediaName, int mediaSizeMB)
        {
            MediaName = mediaName;
            MediaSizeMB = mediaSizeMB;
        }

        public string? MediaName { get; set; }
        public int MediaSizeMB { get; set; }

        public override string ToString()
        {
            return $"Media name:{MediaName}\nSize:{MediaSizeMB} mb";
        }
    }
    abstract public class Storage
    {
        protected Storage(MediaFile? media, string? model, int memorySizeMB)
        {
            Media.Add(media);
            if (model != null) { CurrentSizeMB += media.MediaSizeMB; }
            Model = model;
            MemorySizeMB = memorySizeMB;
        }
        public List<MediaFile?> Media { get; set; } = new();
        public string? Model { get; set; }
        public int MemorySizeMB { get; set; }
        public int CurrentSizeMB { get; set; } = 0;
        public override string ToString()
        {
            return $"Storage type:{GetType().Name}\nModel name:{Model}\nMax storage size(MB):{MemorySizeMB} mb\nUsed size is {CurrentSizeMB} mb\n";
        }
        virtual public void SendFilesTo(Storage other)
        {
            for (int i = 0; i < Media.Count; i++)
            {
                if (CurrentSizeMB + Media[i].MediaSizeMB > MemorySizeMB)
                {
                    Console.WriteLine("Not enough memory!");
                    break;

                }
              other.CurrentSizeMB += Media[i].MediaSizeMB;
                other.Media.Add(this.Media[i]);
            }

        }

        virtual public void ShowMediaFiles()
        {
            foreach (var item in Media)
            {
                Console.WriteLine(item);
            }
        }
        virtual public void ShowStorageInfo()
        {
            Console.WriteLine(ToString());
        }
    }


    public class Flash : Storage
    {
        public Flash(MediaFile? media, string? model, int memorySizeMB) : base(media, model, memorySizeMB) { }
        public override void SendFilesTo(Storage other)
        {
            base.SendFilesTo(other);
        }
        public override void ShowMediaFiles()
        {
            base.ShowMediaFiles();
        }
        public override void ShowStorageInfo()
        {
            base.ShowStorageInfo();
        }
    }

    public class HDD : Storage
    {
        public HDD(MediaFile? media, string? model, int memorySizeMB) : base(media, model, memorySizeMB) { }
        public override void SendFilesTo(Storage other)
        {
            base.SendFilesTo(other);
        }
        public override void ShowMediaFiles()
        {
            base.ShowMediaFiles();
        }
        public override void ShowStorageInfo()
        {
            base.ShowStorageInfo();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // 2 storage-den inheritence olmuw "HDD" ve "Flash" obyekti yaradib
            Flash fcard = new Flash(new MediaFile("Titanicin chenzurasiz kino", 2048), "StepFLashCard", 128000);
            HDD fcard2 = new HDD(new MediaFile("Birwey", 128), "StepHDD", 1280000);
            // Flash kart obyektindeki file-i HDD obyektine abstract storage clasinin SendFileTo(Storage other) funksiyasi
            //vasitesi ile gonderirik.
            fcard.SendFilesTo(fcard2);
            //Media Melumatlarini ekrana chixardiriq
            fcard2.ShowMediaFiles();
            //Storage haqqindaki melumatlari ekrana chixardiriq
            fcard2.ShowStorageInfo();
        }
    }

}