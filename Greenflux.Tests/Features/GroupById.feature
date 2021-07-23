Feature: Group by id scenarios

    Scenario: Get nonexistent group
        When I GET to /v1/groups/99
        Then I should receive the status code 404
        And The response data should be null
        And The response errors should have the code 901

    Scenario: Get existent group
        When I GET to /v1/groups/1
        Then I should receive the status code 200
        And The response data should have a id property with the value 1
        And The response data should have a name property containing group1
        And The response data should have a capacity property with the value 10

    Scenario: Get existent group with decimal capacity
        When I GET to /v1/groups/4
        Then I should receive the status code 200
        And The response data should have a id property with the value 4
        And The response data should have a name property containing group4
        And The response data should have a capacity property with the value 5.5