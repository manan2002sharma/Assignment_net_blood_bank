****Blood Donation Management API****

These API provides functionalities for managing blood donor records using in memory list.
* ****All the screenshot are provided in ScreenShots folder.****
<br>
<br>

**Endpoints**

**1. Add Blood**
* **Endpoint:** `/api/blood`
* **HTTP Method:** POST
* **Description:** Adds a new blood donor record to the system.
* **Request Parameters:**
    * `DonorName` (string, required): Name of the donor.
    * `age` (int, required): Age of the donor (must be positive).
    * `bloodType` (string, required): Blood type in valid format (e.g., "A+", "B-", etc.).
    * `mobNo` (string, required): Mobile number of the donor (must be a valid 10-digit number).
    * `quantity` (int, required): Quantity of blood donated.
    * `CollectionDate` (DateTime, required): Date of blood collection.
    * `ExpDate` (DateTime, required): Expiration date of the blood.
    * `status` (string, required): Current status (must be "Available", "Requested", or "Expired").
* **Response:**
    * **200 OK:** Returns the updated list of blood records.
    * **400 Bad Request:** Validation failed. Error message is returned. Reasons for bad request include:
        * Invalid blood type format.
        * Invalid mobile number format.
        * Non-positive age or quantity.
        * Empty donor name.
        * Invalid status value (not "Available", "Requested", or "Expired").
<br>

**2. Get All Blood Data**
* **Endpoint:** `/api/blood`
* **HTTP Method:** GET
* **Description:** Retrieves all blood donor records in the system.
* **Response:**
    * **200 OK:** Returns a list of all blood donor records.
    * **404 Not Found:** No blood donor records exist in the system.
    
<br>

**3. Delete Blood**
* **Endpoint:** `/api/blood/{id}`
* **HTTP Method:** DELETE
* **Path Parameter:**
    * `{id}` (int, required): The ID of the blood record to delete.
* **Description:** Deletes a specific blood donor record from the system.
* **Response:**
    * **200 OK:** Returns the updated list of blood records (without the deleted record).
    * **404 Not Found:** No blood donor record with the specified ID exists.
    * **400 Bad Request:** Invalid ID format (must be a number).
<br>
<br>

**4. Get Blood by ID**
* **Endpoint:** `api/blood/{id}`
* **HTTP Method:** GET
* **Path Parameter:**
    * `{id}` (int, required): The ID of the blood record to retrieve.
* **Description:** Retrieves a specific blood donor record by its ID.
* **Response:**
    * **200 OK:** Returns the blood donor record if found.
    * **404 Not Found:** No blood donor record with the specified ID exists.
    * **400 Bad Request:** Invalid ID format (must be a number).
<br>
<br>


**5. Update Blood**
* **Endpoint:** `api/blood/{id}`
* **HTTP Method:** PUT
* **Path Parameter:**
    * `{id}` (int, required): The ID of the blood record to update.
* **Request Parameters (Optional):**
    * `newStatus` (string): Updated status ("Available", "Requested", or "Expired").
    * `BloodType` (string): Updated blood type.
    * `age` (int): Updated age (must be positive).
    * `mobNo` (string): Updated mobile number (must be a valid 10-digit number).
    * `DonorName` (string): Updated donor name.
* **Description:** Updates an existing blood donor record. Pass only the parameters you want to modify.
* **Response:**
    * **200 OK:** Returns the updated blood donor record.
    * **400 Bad Request:**
        * Invalid blood type format.
        * Invalid mobile number format.
        * Non-positive age.
        * Invalid status value (not "Available", "Requested", or "Expired").
    * **404 Not Found:** No blood donor record with the specified ID exists.
<br>
<br>


**6. Get Paginated Blood Data**
* **Endpoint:** `api/blood/getPageData`
* **HTTP Method:** GET
* **Query Parameters (Optional):**
    * `page` (int): Page number (default: 1)
    * `size` (int): Page size (default: 10)
* **Description:** Retrieves paginated blood donor records.
* **Response:**
    * **200 OK:** Returns a list of blood donor records for the specified page and size.
    * **404 Not Found:** No blood donor records exist in the system.
<br>
<br>


**7. Search by Blood Type**
* **Endpoint:** `api/blood/bloodtype`
* **HTTP Method:** GET
* **Query Parameter:**
    * `bloodtype` (string, required): Blood type to search for.
* **Description:** Searches for blood donor records by blood type.
* **Response:**
    * **200 OK:** Returns a list of blood donor records matching the blood type.
    * **400 Bad Request:** Invalid blood type format.
    * **404 Not Found:** No blood donor records match the specified blood type.
<br>
<br>

**8. Search by Status**
* **Endpoint:** `api/blood/status`
* **HTTP Method:** GET
* **Query Parameter:**
    * `status` (string, required): Status to search for ("Available", "Requested", or "Expired").
* **Description:** Searches for blood donor records by status.
* **Response:**
    * **200 OK:** Returns a list of blood donor records matching the status.
    * **400 Bad Request:** Invalid status value.
    * **404 Not Found:** No blood donor records match the specified status.
<br>
<br>

**9. Search by Donor Name**
* **Endpoint:** `api/blood/name`
* **HTTP Method:** GET
* **Query Parameter:**
    * `name` (string, required): Donor name to search for.
* **Description:** Searches for blood donor records by donor name.
* **Response:**
    * **200 OK:** Returns a list of blood donor records matching the donor name.
    * **400 Bad Request:** Empty donor name.
    * **404 Not Found:** No blood donor records match the specified donor name.
<br>
<br>

**10. Sort by Collection Date**
* **Endpoint:** `api/blood/sortByDate`
* **HTTP Method:** GET
* **Description:** Returns a list of blood donor records sorted by collection date.
* **Response:**
    * **200 OK:** Returns a sorted list of blood donor records.
    * **404 Not Found:** No blood donor records exist in the system.
<br>
<br>

**11. Sort by Blood Type**
* **Endpoint:** `api/blood/sortByBlood`
* **HTTP Method:** GET
* **Description:** Returns a list of blood donor records sorted by blood type.
* **Response:**
    * **200 OK:** Returns a sorted list of blood donor records.
    * **404 Not Found:** No blood donor records exist in the system.
<br>
<br>

**12. Filter Blood Data**
* **Endpoint:** `api/blood/GetFilterData`
* **HTTP Method:** GET
* **Query Parameters (Optional):**
    * `BloodType` (string): Filter by blood type.
    * `status` (string): Filter by status ("Available", "Requested", or "Expired").
    * `donorName` (string): Filter by donor name (partial match).
* **Description:** Filters blood donor records based on the provided parameters.
* **Response:**
    * **200 OK:** Returns a list of filtered blood donor records.
    * **400 Bad Request:** Invalid filter parameters.
    * **404 Not Found:** No blood donor records match the filter criteria.
    
