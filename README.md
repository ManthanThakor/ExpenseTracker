# ExpenseTracker

A comprehensive expense tracking application built with ASP.NET Core MVC to help you manage your personal finances.

![Expense Tracker Demo](https://github.com/ManthanThakor/ExpenseTracker/blob/main/ExpenseTrackerMvc/wwwroot/imgs/Ex-tr-img.png)

## Overview

ExpenseTracker is a web application designed to help users track their daily expenses, categorize spending, and visualize financial habits. Built on ASP.NET Core MVC architecture with Entity Framework Core for data management, it offers a robust solution for personal finance management.

## Features

- **Expense Logging**: Quickly add new expenses with date, amount, category, and description
- **Income Tracking**: Record your income sources to get a complete financial picture
- **Categorization**: Organize transactions by customizable categories
- **Visual Reports**: View your spending patterns with charts and graphs
- **User Authentication**: Secure user accounts with registration and login
- **Responsive Design**: Works seamlessly across desktop and mobile devices

## Technologies Used

- **Backend**:
  - ASP.NET Core MVC
  - Entity Framework Core
  - SQL Server/SQL Database
  - C#
  - LINQ

- **Frontend**:
  - HTML5
  - CSS3
  - JavaScript
  - Bootstrap
  - jQuery
  - Chart.js (for visualizations)

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/ManthanThakor/ExpenseTracker.git
   ```

2. Navigate to the project directory:
   ```
   cd ExpenseTracker
   ```

3. Ensure you have the .NET Core SDK installed

4. Update the connection string in `appsettings.json` to point to your SQL Server instance

5. Apply database migrations:
   ```
   dotnet ef database update
   ```

6. Run the application:
   ```
   dotnet run
   ```

7. Navigate to `https://localhost:5001` in your web browser

## Usage

1. Register for a new account or login with existing credentials
2. Add a new expense by filling out the expense form
3. View your expenses in the transaction history
4. Check the dashboard to see spending patterns and budget status
5. Use the filter options to find specific transactions

## Project Structure

```
ExpenseTracker/
├── Controllers/       # MVC Controllers
├── Models/            # Data models
│   ├── Domain/        # Entity models
│   └── ViewModels/    # View-specific models
├── Views/             # Razor views
├── Data/              # EF Core context and migrations
├── Services/          # Business logic services
├── wwwroot/           # Static files (CSS, JS, images)
│   ├── css/
│   ├── js/
│   └── images/
├── Program.cs         # Application entry point
├── Startup.cs         # Application configuration
└── README.md          # Project documentation
```

## Future Enhancements

- Multi-currency support
- Receipt scanning and OCR
- Recurring transaction setup
- Advanced reporting features
- Mobile application
- API for third-party integrations

## Contributing

Contributions are welcome! If you'd like to improve ExpenseTracker:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact

- GitHub: [ManthanThakor](https://github.com/ManthanThakor)
- Project Link: [https://github.com/ManthanThakor/ExpenseTracker](https://github.com/ManthanThakor/ExpenseTracker)

## Acknowledgments

- Inspired by personal finance management best practices
- Thanks to the ASP.NET Core and EF Core communities
