namespace ProjectRequirements.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    [Table("_Users")]
    public partial class C_Users : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ID"));
            }
        }


        private string _UserName;


        [StringLength(250)]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }


        private string _UserPassword;
        [StringLength(250)]
        public string UserPassword
        {
            get { return _UserPassword; }
            set
            {
                _UserPassword = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserPassword"));
            }
        }


        private byte[] _UserImage;
        public byte[] UserImage
        {
            get { return _UserImage; }
            set
            {
                _UserImage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserImage"));
            }
        }


        private string _Mobile;
        [StringLength(50)]
        public string Mobile
        {
            get { return _Mobile; }
            set
            {
                _Mobile = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mobile"));
            }
        }
        private string _Notes;
        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("_Notes"));
            }
        }
    }
}
