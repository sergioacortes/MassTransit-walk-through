#!/bin/bash

base_path=$PWD

print_menu() {

    clear

    echo ""
    echo "Select an option:"
    echo ""
    echo "1.- infrastructure"
    echo "2.- infrastructure-messaging"
    echo "3.- Infrastructure-microsoft"
    echo "4.- Infrastructure-mongodb"
    echo "5.- Infrastructure-monitoring"
    echo ""
    echo "51.- infrastructure"
    echo "52.- infrastructure-messaging"
    echo "53.- Infrastructure-microsoft"
    echo "54.- Infrastructure-mongodb"
    echo "55.- Infrastructure-monitoring"
    echo ""
    echo "0.- Salir"
    echo ""

    read -p "Select and option: " option_selected

    return $option_selected

}

docker_compose_up() {
   
    local docker_compose="$base_path/docker-compose.yaml"
 
    local profile=$1
    local domain=$2

    docker-compose --file $docker_compose --profile $profile up -d

}

docker_compose_down() {
   
    local docker_compose="$base_path/docker-compose.yaml"

    local profile=$1

    docker-compose --file $docker_compose --profile $profile down

}

print_menu
current_selection=$?

while [[ $current_selection -gt 0 ]];  
do  

    case $current_selection in

        1)
            docker_compose_up "infrastructure"
            print_menu
            current_selection=$?
        ;;

        2)
            docker_compose_up "infrastructure-messaging"
            print_menu
            current_selection=$?
        ;;

        3)
            docker_compose_up "infrastructure-microsoft"
            print_menu
            current_selection=$?
        ;;

        4)
            docker_compose_up "infrastructure-mongodb"
            print_menu
            current_selection=$?
        ;;

        5)
            docker_compose_up "infrastructure-monitoring"
            print_menu
            current_selection=$?
        ;;

        51)
            docker_compose_down "infrastructure"
            print_menu
            current_selection=$?
        ;;

        52)
            docker_compose_down "infrastructure-messaging"
            print_menu
            current_selection=$?
        ;;

        53)
            docker_compose_down "infrastructure-microsoft"
            print_menu
            current_selection=$?
        ;;

        54)
            docker_compose_down "infrastructure-mongodb"
            print_menu
            current_selection=$?
        ;;

        55)
            docker_compose_down "infrastructure-monitoring"
            print_menu
            current_selection=$?
        ;;

    esac

done  