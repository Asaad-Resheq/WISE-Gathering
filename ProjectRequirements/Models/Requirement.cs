namespace ProjectRequirements.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Requirement : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged  implementation ..
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Requirement()
        {
            
        }

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
        private string _ReqText;
        public string ReqText
        {
            get { return _ReqText; }
            set
            {
                _ReqText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ReqText"));
            }
        }


        private int? _StickholderID;
        public int? StickholderID
        {
            get { return _StickholderID; }
            set
            {
                _StickholderID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StickholderID"));
            }
        }

        private DateTime? _CreattionDate;
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreattionDate
        {
            get { return _CreattionDate; }
            set
            {
                _CreattionDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CreattionDate"));
            }
        }

        private DateTime? _LastModificationDate;
        [Column(TypeName = "smalldatetime")]
        public DateTime? LastModificationDate
        {
            get { return _LastModificationDate; }
            set
            {
                _LastModificationDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastModificationDate"));
            }
        }

        private string _BaseKeyWord;
        [StringLength(50)]
        public string BaseKeyWord
        {
            get { return _BaseKeyWord; }
            set
            {
                _BaseKeyWord = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BaseKeyWord"));
            }
        }
        private int? _ProjectID;
        public int? ProjectID
        {
            get { return _ProjectID; }
            set
            {
                _ProjectID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProjectID"));
            }
        }
        private  Project _Project;
        public virtual Project Project
        {
            get { return _Project; }
            set
            {
                _Project = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Project"));
            }
        }

        


        private Stickholder _Stickholder;
        public virtual Stickholder Stickholder
        {
            get { return _Stickholder; }
            set
            {
                _Stickholder = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Stickholder"));
            }
        }

        
    }
}
