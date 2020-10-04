# augen-book-store

## Decision making
### Backend
- BookModel & Controller: Model was built with 6 fields including "ID", "Title", "Author", "Description", "Image", "PublishedDate". The "ID" makes the data flow be solid while navigating among variaty of books.
- DriverModel & Controller: Model was built with 2 fields including "DriverName", "PhoneNo" so that when I random a Driver in BuyBookController for Motobike service, I can get the DriverName along with PhoneNumber. Train and Aircraft services in this situation no need to build model, trainNo and flightNo is generated randomly when generating delivery info in BuyBookController.
- BuyBookModel & Controller: Model was built with 2 fields including "DeliveryService" and "DeliveryCost", calculate DeliveryCost and generating delivery info.  

### Frontend
- BookList component: rendering a list of books with the searching book by title and author
- BookDetail component: render book detail and allow user to select delivery service and buy. Bookdetail component code can be placed in BookList component but to make the code clear and solid, separating in another component is necessary. 
- formatDate function in helper.js: to format dateTime data (PublishedDate) that can be reused in both BookList and BookDetail components, reducing code duplicated.

### Development Pattern

- Feature: Backend -> Frontend -> Minor styling -> Testing (Flow) -> Fixing
- Final: Final Styling -> Final testing and fixing

![CodingPatternDiagram](https://user-images.githubusercontent.com/48636234/95029807-5d838d00-0707-11eb-810f-e1690498becd.jpg)


## Summary breakdown
- Preparation: 1 hour
- Backend: 6 hours
- Frontend: 7 hours
- Styling: 1hours
->Grand total: 15 hours
