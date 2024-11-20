using System.Text.RegularExpressions;
using Assignment_net_blood_bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_net_blood_bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bloodController : ControllerBase
    {
        //This is in the list which will be used for all manipulation
        //For this blood model is created in Models folder
        public static List<blood> bloods= new List<blood>
        {
            new blood { Id = 1, age = 22, BloodType = "A+",ContactInfo="1234567890",DonorName="Manan",CollectionDate=  DateTime.Now, Quantity=3, ExpiratoinDate = DateTime.Now.AddDays(40), Status = "Available" },
            new blood { Id = 2, age = 22, BloodType = "B+",ContactInfo="1234567890",DonorName="Chikku",CollectionDate=  DateTime.Now, Quantity=3, ExpiratoinDate = DateTime.Now.AddDays(40), Status = "Available" }
        };


        //I have defined functions here for validation of each field to check for data validation 
        private bool Statuscheck(string status)
        {
            if (status == "Available" || status == "Requested" || status == "Expired") return true;
            return false;
        }

        private bool BloodTypecheck(string bloodtype)
        {
            if (bloodtype=="A+"||bloodtype == "B-"|| bloodtype=="B+"||bloodtype == "A-"||bloodtype == "AB+"||bloodtype == "AB-"||bloodtype == "O+"||bloodtype == "O-")
            {
                return true;
            }
            return false;
        }
        private bool MobnoCheck(string mobno)
        {
            string pattern = @"^\d{10}$";
            if (Regex.IsMatch(mobno, pattern)) return true;
            return false;
        }


        //This is post endpoint which will add new entry in data
        [HttpPost]
        public ActionResult<IEnumerable<blood>> AddBlood(string DonornName,int age , string bloodType, string mobNo, int quantity , DateTime CollectionDate, DateTime ExpDate, string status )
        {
            //Doing basic validation of data using function defined earlier and sending badreq if not in correct form
            if (!Statuscheck(status))
            {
                return BadRequest("Status can only be - Availabe, Requested or Expired");
            }
            if (!BloodTypecheck(bloodType))
            {
                return BadRequest("Enter a valid blood type in capital alphabet with sign after it ");
            }
            if (!MobnoCheck(mobNo))
            {
                return BadRequest("Enter a valid Mobile Number");
            }
            if (age <= 0 || quantity <= 0 || string.IsNullOrEmpty(DonornName))
            {
                return BadRequest("Check if age or quantity is negative or zero or else Donor Name not provided");
            }

            //generating unique id 
            int id = 1;
            if(bloods != null && bloods.Count > 0)
            {
                id = bloods.Max(i => i.Id) + 1;
            }

            //adding data to bloods list
            bloods.Add(new blood {Id = id, age = age,DonorName = DonornName,BloodType=bloodType,ContactInfo=mobNo,Quantity=quantity,CollectionDate=CollectionDate,ExpiratoinDate=ExpDate,Status=status });
            //returning complete bloods with result
            return Ok(bloods);
        }

        //This endpoint is to get all data of blood bank
        [HttpGet]
        public ActionResult<IEnumerable<blood>> GetBloodData()
        {
            //if no blood is found return Notfound else return data
            if (bloods == null) return NotFound("Empty no Data Entered");
            return Ok(bloods);
        }



        //This endpoint find data with same id
        [HttpGet("id")]
        public ActionResult<blood> GetBloodById(int id)
        {
            //It checks if blood donor entry with given id exist if not then return notfound else return data
            if(bloods.Count<=0) return NotFound("Empty no Data Entered");
            var blood = bloods.Find(i => i.Id == id);
            if (blood == null) return NotFound("No data with id found");
            return blood;
        }



        //This endpoint is to update the data based on id which is req field and everything else is optinal what ever is given in optinal field will be updated for given id
        [HttpPut("id")]
        public ActionResult<blood> updateBloodById(int id, string? newStatus, string? BloodType, int? age , string? mobNo , string? DonorName)
        {

            //First we are doing validation for each optional input if given
            if (newStatus!=null && !Statuscheck(newStatus))
            {
                return BadRequest("Status can only be - Availabe, Requested or Expired");
            }
            if (BloodType!=null && !BloodTypecheck(BloodType))
            {
                return BadRequest("Enter a valid blood type in capital alphabet with sign after it ");
            }
            if (mobNo!=null && !MobnoCheck(mobNo))
            {
                return BadRequest("Enter a valid Mobile Number");
            }
            if (age!=null && age <= 0 )
            {
                return BadRequest("Check if age or quantity is negative or zero or else Donor Name not provided");
            }


            //if no data is present
            if (bloods.Count <= 0) return NotFound("Empty no Data Entered");
            

            //Find the data check if given optional filed is not null and if so update it and return updated data
            var blood = bloods.Find(i => i.Id == id);
            if (blood == null) return NotFound("No data with id found");
            if (newStatus != null) blood.Status= newStatus;
            if(mobNo!=null ) blood.ContactInfo= mobNo;
            if (DonorName != null) blood.DonorName = DonorName;
            if (age != null) blood.age = (int)age;
            if(BloodType!=null) blood.BloodType = BloodType;

            return blood;
        }


        //This endpoint is to delete data based on id
        [HttpDelete]
        public ActionResult<IEnumerable<blood>> DeleteBloodById(int id)
        {

            //Check if data exist with id if not then return not found
            if (bloods.Count <= 0) return NotFound("Empty no Data Entered");
            var blood = bloods.Find(i => i.Id == id);
            if (blood == null) return NotFound("No data with id found");

            //remove the blood from data and return updated data
            bloods.Remove(blood);
            return bloods;
        }


        //This api endpoint code is for pagination
        [HttpGet("getPageData")]
        public ActionResult<IEnumerable<blood>> getPageData(int page = 1, int size = 10)
        {
            //simply skip to the number of pages given and then fetch data on basis of size
            var res = bloods.Skip((page - 1) * size).Take(size).ToList();
            return res;
        }


        //Now there are three endpoints to search data based on bloodtype , status , DonorName
        //for all three mostly same logic is used first validate the input field 
        //then by using where linq query find the data and return it
        [HttpGet("bloodtype")]
        public ActionResult<IEnumerable<blood>> SearchByBlood(string bloodtype)
        {
            if (!BloodTypecheck(bloodtype))
            {
                return BadRequest("Enter a valid blood type in capital alphabet with sign after it ");
            }
            var bloodRes = bloods.Where(i => i.BloodType == bloodtype).ToList();
            if (bloodRes == null) return NotFound();
            return bloodRes;
        }

        [HttpGet("status")]
        public ActionResult<IEnumerable<blood>> SearchByStatus(string status)
        {
            if (!Statuscheck(status))
            {
                return BadRequest("Status can only be - Availabe, Requested or Expired");
            }
            var bloodRes = bloods.Where(i => i.Status == status).ToList();
            if (bloodRes == null) return NotFound();
            return bloodRes;
        }
        [HttpGet("name")]
        public ActionResult<IEnumerable<blood>> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name can't be empty");
            }
            var bloodRes = bloods.Where(i => i.DonorName == name).ToList();
            if (bloodRes == null) return NotFound();
            return bloodRes;
        }


        //Bonus Part

        //There are two enpoints first to sort by date and another to sort by bloodtype
        //here simply we first check if bloodList has data if it having data then simply use orderby and return value simple.
        [HttpGet("sortByDate")]
        public ActionResult<IEnumerable<blood>> SortByDate()
        {
            if (bloods == null || bloods.Count == 0) return NotFound("No data available.");

            var sortedBloods = bloods.AsQueryable();

            sortedBloods= sortedBloods.OrderBy(i=>i.CollectionDate);

            return Ok(sortedBloods);
        }
        [HttpGet("sortByBlood")]
        public ActionResult<IEnumerable<blood>> SortByBlood()
        {
            if (bloods == null || bloods.Count == 0) return NotFound("No data available.");

            var sortedBloods = bloods.AsQueryable();

            sortedBloods = sortedBloods.OrderBy(i => i.BloodType);

            return Ok(sortedBloods);
        }



        //This endpoint is to return data filtered based on multiple parameters
        [HttpGet("GetFilterdData")]
        public ActionResult<IEnumerable<blood>> GetBloodFilteredData(string? BloodType = null,string? status = null,string? donorName = null)
        {

            //first we check if user has provided data then it should be valid
            if (status != null && !Statuscheck(status))
            {
                return BadRequest("Status can only be - Availabe, Requested or Expired");
            }
            if (BloodType != null && !BloodTypecheck(BloodType))
            {
                return BadRequest("Enter a valid blood type in capital alphabet with sign after it ");
            }

            //if no data is present return notfound
            if (bloods == null || bloods.Count == 0) return NotFound("No data available.");

            var filteredBloods = bloods.AsQueryable();


            //simply by using where we can filter data based on our requirements
            if (!string.IsNullOrEmpty(BloodType))
            {
                filteredBloods = filteredBloods.Where(b => b.BloodType.Equals(BloodType, StringComparison.OrdinalIgnoreCase));
            }
                

            if (!string.IsNullOrEmpty(status))
            {
                filteredBloods = filteredBloods.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(donorName))
            {
                filteredBloods = filteredBloods.Where(b => b.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(filteredBloods.ToList());
        }
    }
}
