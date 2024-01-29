using ConsoleApp.Model;
using ConsoleApp.Model.Enum;
using ConsoleApp.OutputTypes;

namespace ConsoleApp;

public class QueryHelper : IQueryHelper
{
    /// <summary>
    /// Get Deliveries that has payed
    /// </summary>
    public IEnumerable<Delivery> Paid(IEnumerable<Delivery> deliveries) //TODO: Завдання 1
    {
        return deliveries.Where(d => d.PaymentId != null);
    }

    /// <summary>
    /// Get Deliveries that now processing by system (not Canceled or Done)
    /// </summary>
    public IEnumerable<Delivery> NotFinished(IEnumerable<Delivery> deliveries) //TODO: Завдання 2
    {
        return deliveries.Where(delivery => delivery.Status != DeliveryStatus.Cancelled && delivery.Status != DeliveryStatus.Done);
    }
    /// <summary>
    /// Get DeliveriesShortInfo from deliveries of specified client
    /// </summary>
    public IEnumerable<DeliveryShortInfo> DeliveryInfosByClient(IEnumerable<Delivery> deliveries, string clientId) //TODO: Завдання 3
    {
        return deliveries
            .Where(delivery => delivery.ClientId == clientId)
            .Select(delivery => new DeliveryShortInfo
            {
                Id = delivery.Id,
                ClientId = delivery.ClientId,
                StartCity = delivery.Direction?.Origin?.City,
                EndCity = delivery.Direction?.Destination?.City,
                Type = delivery.Type,
                LoadingPeriod = delivery.LoadingPeriod,
                ArrivalPeriod = delivery.ArrivalPeriod,
                Status = delivery.Status,
                CargoType = delivery.CargoType
            });
    }
    /// <summary>
    /// Get first ten Deliveries that starts at specified city and have specified type
    /// </summary>
    public IEnumerable<Delivery> DeliveriesByCityAndType(IEnumerable<Delivery> deliveries, string cityName, DeliveryType type) => new List<Delivery>();//TODO: Завдання 4
    /// <summary>
    /// Order deliveries by status, then by start of loading period
    /// </summary>
    public IEnumerable<Delivery> OrderByStatusThenByStartLoading(IEnumerable<Delivery> deliveries) => new List<Delivery>();//TODO: Завдання 5
    /// <summary>
    /// Count unique cargo types
    /// </summary>
    public int CountUniqCargoTypes(IEnumerable<Delivery> deliveries) //TODO: Завдання 6
    {
        return deliveries.Select(delivery => delivery.CargoType).Distinct().Count();
    }
    /// <summary>
    /// Group deliveries by status and count deliveries in each group
    /// </summary>
    public Dictionary<DeliveryStatus, int> CountsByDeliveryStatus(IEnumerable<Delivery> deliveries) //TODO: Завдання 7
    {
        return deliveries.GroupBy(delivery => delivery.Status).ToDictionary(group => group.Key, group => group.Count());
    }

    /// <summary>
    /// Group deliveries by start-end city pairs and calculate average gap between end of loading period and start of arrival period (calculate in minutes)
    /// </summary>
    public IEnumerable<AverageGapsInfo> AverageTravelTimePerDirection(IEnumerable<Delivery> deliveries) => new List<AverageGapsInfo>();//TODO: Завдання 8

    /// <summary>
    /// Paging helper
    /// </summary>
    public IEnumerable<TElement> Paging<TElement, TOrderingKey>(IEnumerable<TElement> elements,
        Func<TElement, TOrderingKey> ordering,
        Func<TElement, bool>? filter = null,
        int countOnPage = 100,
        int pageNumber = 1) //TODO: Завдання 9
    {
        // Фільтрація
        if (filter != null)
        {
            elements = elements.Where(filter);
        }

        // Сортування
        elements = elements.OrderBy(ordering);

        // Пагінація
        return elements.Skip((pageNumber - 1) * countOnPage).Take(countOnPage);
    }
}
