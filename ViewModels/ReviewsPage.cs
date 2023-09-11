using System.Collections;


namespace PracticEPAM.ViewModels
{
    public class ReviewsPage:IEnumerable
    {
        public PracticEPAM.Models.Product Product { get; set; }    
        public List<PracticEPAM.Models.Review> Reviews { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
