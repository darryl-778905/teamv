MobilePoll.DataModel
====================

The datamodel defines what will be stored in the database. As far as is practical, this should be independant of any specific persistence technology.
Specific persistence mechanisms such as Entity Framework or ADO.net should be abstracted via the repository pattern and implemented in a seperate project.
