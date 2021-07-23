Feature: Group creation scenarios

    Scenario: Create group with null values
        When I use the payload
        """
        {
            "name": null,
            "capacity": null
        }
        """
        And I POST to /v1/groups
        Then I should receive the status code 400
        And The response errors should have 2 items
        And The response has 1 errors with code 900 containing name
        And The response has 1 errors with code 900 containing capacity

    Scenario: Create group with null capacity
        When I use the payload
        """
        {
            "name": "group1",
            "capacity": null
        }
        """
        And I POST to /v1/groups
        Then I should receive the status code 400
        And The response errors should have 1 items
        And The response has 1 errors with code 900 containing capacity

    Scenario: Create group with null name
        When I use the payload
        """
        {
            "name": null,
            "capacity": 10
        }
        """
        And I POST to /v1/groups
        Then I should receive the status code 400
        And The response errors should have 1 items
        And The response has 1 errors with code 900 containing name

    Scenario: Create group with invalid capacity
        When I use the payload
        """
        {
            "name": "group1",
            "capacity": "capacity"
        }
        """
        And I POST to /v1/groups
        Then I should receive the status code 400
        And The response errors should have 1 items
        And The response has 1 errors with code 900 containing capacity

    Scenario: Create group
        When I use the payload
        """
        {
            "name": "group6",
            "capacity": 10
        }
        """
        And I POST to /v1/groups
        Then I should receive the status code 201
        And The response data should have a id property with the value 7
        And The response data should have a name property with the value group6
        And The response data should have a capacity property with the value 10
        And The created group should exists in the database with the correct values