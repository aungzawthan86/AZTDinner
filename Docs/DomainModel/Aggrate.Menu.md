# Domain Models

## Menu

```csharp

class Menu
{
    Menu Create();
    void AddDinner(Dinner dinner);
    void RemoveDinner(Dinner dinner);
    void UpdateSection(Section section);
}

```

```json
{
  "id": "000000-0000-0000",
  "name": "Yummy Menu",
  "description": "A menu with yummy food",
  "averageRating": 4.5,
  "sections": [
    {
      "id": "000000-0000-0000",
      "name": "Appetizers",
      "description": "Starters",
      "items": [
        {
          "id": "000000-0000-0000",
          "name": "Fried Pickles",
          "description": "Deep fried pickles",
          "price": 5.99
        }
      ]
    }
  ],
  "createdDateTime": "2023-06-15T13:45:30",
  "updatedDateTime": "2023-06-15T13:45:30",
  "hostId": "000000-0000-0000",
  "dinnerIds": ["000000-0000-0000"],
  "manuReviewIds": ["000000-0000-0000"]
}
```
