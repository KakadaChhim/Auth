using static Authentication.Model.ShareModel;

namespace Authentication.Model
{
    public class BranchModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
    public class BranchAddModel: BranchModel { }
    public class BranchListModel: BranchModel 
    {
        public long Id { get; set; }
    }
    public class BranchEditModel: BranchModel 
    {
        public long Id { get; set; }
    }
    public class BranchViewModel: BranchModel 
    {
        public long Id { get; set; }
    }
    public class BranchRemove: RemoveModel { }
}
