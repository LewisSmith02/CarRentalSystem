[Serializable]
public class Vehicle
{
    // DATA
    private string regPlate = "XX00 XXX";
    private string vehicleMake = "MAKE";
    private string vehicleModel = "MODEL";
    private int rentalPrice = 50;
    private int depositPrice = 100;
    private int availability = 0; // 1 = available, 0 = not available 

    // CONSTRUCTORS
    public Vehicle(string regPlate, string vehicleMake, string vehicleModel, int rentalPrice, int depositPrice, int availability)
    {
        this.regPlate = regPlate;
        this.vehicleMake = vehicleMake;
        this.vehicleModel = vehicleModel;
        this.rentalPrice = rentalPrice;
        this.depositPrice = depositPrice;
        this.availability = availability;
    }

    public Vehicle()
    {

    }

    // FUNCTIONS
    public string GetVehicleRegPlate()
    {
        return regPlate;
    }
    public string GetVehicleMake()
    {
        return vehicleMake;
    }
    public string GetVehicleModel()
    {
        return vehicleModel;
    }
    public int GetRentalPrice()
    {
        return rentalPrice;
    }
    public int GetFinalRentalPrice(int vehicleRentalDuration)
    {
        return rentalPrice * vehicleRentalDuration;
    }
    public int GetDepositPrice()
    {
        return depositPrice;
    }
    public int GetAvailability()
    {
        return availability;
    }
    public void SetVehicleRegPlate(string regPlate)
    {
        this.regPlate = regPlate;
    }
    public void SetVehicleMake(string vehicleMake)
    {
        this.vehicleMake = vehicleMake;
    }
    public void SetVehicleModel(string vehicleModel)
    {
        this.vehicleModel = vehicleModel;
    }
    public void SetRentalPrice(int rentalPrice)
    {
        this.rentalPrice = rentalPrice;
    }
    public void SetDepositPrice(int depositPrice)
    {
        this.depositPrice = depositPrice;
    }
    public void SetAvailability()
    {
        this.availability = availability;
    }
    public string GetVehicleMakeAndModel()
    {
        return vehicleMake + " " + vehicleModel;
    }
    public void SetAvailability(int availability)
    {
        this.availability = availability;
    }
}
