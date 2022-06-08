from __future__ import annotations

from typing import Optional

import mysql.connector


class DBHelper:
    __instance = None  # Объект в единственном виде
    __connection = None  # Объект подключения к бд
    __cursor = None  # Курсор в бд

    settings_file = str()  # Путь до файла с настройками

    def __init__(self, user: str, password: str, host: str, database_name: str) -> None:
        if not DBHelper.__instance:
            self.__connection = mysql.connector.connect(
                user=user, password=password, host=host, database=database_name
            )
            self.__cursor = self.__connection.cursor()

            DBHelper.__instance = self

    @staticmethod
    def get_instance() -> DBHelper:
        if not DBHelper.__instance:
            DBHelper(**DBHelper.read_settings_file())

        return DBHelper.__instance

    def __del__(self):
        DBHelper.get_instance().__connection.close()

    @staticmethod
    def login_in(email: str, password: str) -> Optional[dict]:
        query = f"select RoleID from Users where Email = '{email}' and Password = '{password}'"
        DBHelper.get_instance().__cursor.execute(query)

        data = DBHelper.get_instance().__cursor.fetchall()
        if len(data) == 0:
            return None
        else:
            return {"role_id": data[0][0]}

    @staticmethod
    def read_settings_file() -> dict:
        if DBHelper.settings_file == str():
            raise FileNotFoundError

        settings = dict()

        with open(DBHelper.settings_file, "r") as settings_file:
            for line in settings_file:
                line_list = line.split("=")
                settings[line_list[0]] = line_list[1][:-1]  # Убираем перенос строки

        return settings


def main() -> None:
    DBHelper.settings_file = "help_files/database_settings.dk"

    # print(DBHelper.get_instance().login_in("asakura475@gmail.com", "23514317"))
    # print(DBHelper.get_instance().login_in("asakura_user@gmail.com", "23514317"))


if __name__ == "__main__":
    main()
