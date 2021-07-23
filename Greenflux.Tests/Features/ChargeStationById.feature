Feature: Charge Stations by id scenarios

    Scenario: Get nonexistent charge station
        When I GET to /v1/stations/99
        Then I should receive the status code 404
        And The response data should be null
        And The response errors should have the code 902

    Scenario Outline: Get existent charge station
        When I GET to /v1/stations/<id>
        Then I should receive the status code 200
        And The response data should have a id property with the value <id>
        And The response data should have a name property containing <name>
        And The response data should have a groupId property containing <groupId>

        Examples: 
        | id | name       | groupId |
        | 1  | station1.1 | 1       |
        | 2  | station1.2 | 1       |
        | 6  | station4.1 | 4       |
