Feature: Charge station list scenarios

    Scenario: List Charge Stations for nonexistent group
        When I GET to /v1/groups/6/stations
        Then I should receive the status code 200
        And The response data should be empty

    Scenario: List Charge Stations for existing group without stations
        When I GET to /v1/groups/5/stations
        Then I should receive the status code 200
        And The response data should be empty

    Scenario Outline: List Charge Stations for existing group with stations
        When I GET to /v1/groups/<groupId>/stations
        Then I should receive the status code 200
        And The response data should have <count> items

        Examples: 
        | groupId | count |
        | 1       | 2     |
        | 2       | 1     |

    Scenario: List Charge Stations for group
        When I GET to /v1/groups/1/stations
        Then I should receive the status code 200
        And The response data at index 0 should have a name property containing station1.1
        And The response data at index 1 should have a name property containing station1.2