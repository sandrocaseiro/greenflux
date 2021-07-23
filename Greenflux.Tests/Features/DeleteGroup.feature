Feature: Group deletion scenarios

    Scenario: Delete a non existing group
        When I DELETE to /v1/groups/6
        Then I should receive the status code 404
        And The response errors should have 1 items
        And The response errors should have the code 901

    Scenario: Delete a group
        When I DELETE to /v1/groups/1
        Then I should receive the status code 204
        And The group 1 should not exist
        And The charge stations for group 1 should not exist
        And The connectors for group 1 should not exist
