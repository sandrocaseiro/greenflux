Feature: Connector list scenarios

    Scenario: List Connectors for nonexistent group
        When I GET to /v1/groups/6/connectors
        Then I should receive the status code 200
        And The response data should be empty

    Scenario: List Connectors for existent group without stations
        When I GET to /v1/groups/5/connectors
        Then I should receive the status code 200
        And The response data should be empty

    Scenario: List Connectors for existent group without connectors
        When I GET to /v1/groups/4/connectors
        Then I should receive the status code 200
        And The response data should be empty

    Scenario Outline: List Connectors for existent group with connectors
        When I GET to /v1/groups/<groupId>/connectors
        Then I should receive the status code 200
        And The response data should have <count> items

        Examples: 
        | groupId | count |
        | 1       | 4     |
        | 2       | 3     |
        | 3       | 2     |

    Scenario: List Connectors for group
        When I GET to /v1/groups/1/connectors
        Then I should receive the status code 200
        And The response data at index 0 should have a id property with the value 1
        And The response data at index 2 should have a id property with the value 3
        And The response data at index 3 should have a id property with the value 1

        Scenario: List Connectors for nonexistent station
        When I GET to /v1/stations/99/connectors
        Then I should receive the status code 200
        And The response data should be empty

    Scenario: List Connectors for existent station without connectors
        When I GET to /v1/stations/5/connectors
        Then I should receive the status code 200
        And The response data should be empty

    Scenario Outline: List Connectors for existent station with connectors
        When I GET to /v1/stations/<stationId>/connectors
        Then I should receive the status code 200
        And The response data should have <count> items

        Examples: 
        | stationId | count |
        | 1         | 3     |
        | 2         | 1     |
        | 4         | 2     |

    Scenario: List Connectors Stations for station
        When I GET to /v1/stations/1/connectors
        Then I should receive the status code 200
        And The response data at index 0 should have a id property with the value 1
        And The response data at index 1 should have a id property with the value 2
        And The response data at index 2 should have a id property with the value 1