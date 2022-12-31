namespace ProjectRequirements.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Stickholder : INotifyPropertyChanged
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
        public Stickholder()
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



        private string _StickholderName;
        [StringLength(250)]
        public string StickholderName
        {
            get { return _StickholderName; }
            set
            {
                _StickholderName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StickholderName"));
            }
        }

        private string _Mobile;
        [StringLength(250)]
        public string Mobile
        {
            get { return _Mobile; }
            set
            {
                _Mobile = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mobile"));
            }
        }



        private string _Email;

        [MaxLength(250)]
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Email"));
            }
        }


        private int? _ProjectId;

        public int? ProjectId
        {
            get { return _ProjectId; }
            set
            {
                _ProjectId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProjectId"));
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
