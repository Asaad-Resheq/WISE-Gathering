namespace ProjectRequirements.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Project : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Requirements = new HashSet<Requirement>();
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

        private string _Title;
        [StringLength(250)]
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Title"));
            }
        }

        //Company
        private string _Company;
        [StringLength(250)]
        public string Company
        {
            get { return _Company; }
            set
            {
                _Company = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Company"));
            }
        }

        private DateTime? _CreationDate;
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreationDate
        {
            get { return _CreationDate; }
            set
            {
                _CreationDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CreationDate"));
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

        private int? _UserID;
        public int? UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserID"));
            }
        }


        private  ICollection<Requirement> _Requirements;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requirement> Requirements
        {
            get { return _Requirements; }
            set
            {
                _Requirements = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Requirements"));
            }
        }
    }
}
