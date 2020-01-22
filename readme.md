# Budget Expense Recorder

Simple mobile application to make recording expenses easy

### Use Cases

##### Main use cases 

1. Add new expense

    When the app is launched, user enters expense amount, and selects category.
    After that, expense with that amount and date is added to relevant category.

2. Add expense into new category

    After specifying expense amount, user enters name of a category, that does not exist yet.
    Application prompts user to create new category. Then a new category is created with expense in it.
    
3. Add new gain
    
    Gain is treated as a special kind of expense, it has its default category. To add gain,
    after user has entered expense amount it presses "Add expense" button.
   
4. Export data

    User clicks "Export" button, that launches platform-native export. Exported file is a 
    CSV file with information about all expenses in categories.
    
5. Reset (clear) expenses

    After exporting, user will be prompted to either clear expenses, or leave them recorded.
    User can also click button to clear expenses manually.
    
##### Additional use cases

- Opening categories tab

    To open categories tab, user clicks "Categories" button. 

- Opening category context menu

    In categories tab, user taps and holds on selected category for .5s to open context menu. 
    
1. Rename category

    In category context menu, user selects "Rename". After that, user selects new name.
    If no category with such name exists, category is renamed.
    If category with new name exists, user is prompted to merge these two categories.
    
2. Delete category

    In category context menu, user selects "Delete". 
    Then, user is prompted to either move expenses to new category, or delete them.
    
3. Create category

    Category can be created either via adding new expense, or manually in category screen by pressing
    "New category" button and entering (prompted) category name
    
    
### Application components

##### Screens

1. Add expense screen
    
    Contains:
    
    1. Expense amount input field.
    2. Category name input field - to lookup category, or to enter name of new category.
    3. `Gain` button, used to quickly add expense as gain.
    4. Categories buttons, sorted by relevancy and by similarity to entered category name.
    5. `Share` button.
    6. `Categories` button.
    7. `Clear expenses` button.
    
2. Categories screen

   Contains all categories 

##### Prompts

1. Warning prompt, with options `Confirm` and `Cancel`
    1. When renaming to existing category, categories will be merged 
2. Options prompt
    1. For renaming: `[Merge, Cancel]`
    2. For deletion: `[Move to new category, Delete, Cancel]`
3. Input prompt 
    1. For renaming. Has text input field and `[Confirm, Cancel options]`
    2. To create new category
3. Category selection prompt
    1. To move expenses from deleted category 