sed -i "s|ENV|Development|" docker-compose.yml
sed -i "s|CONNECTION_STRING|${{ secrets.CONNECTION_STRING }}|" docker-compose.yml

sed -i "s|APPLICATION_INSIGHTS_KEY|${{ secrets.APPLICATION_INSIGHTS_KEY }}|" docker-compose.yml

sed -i "s|JWT_APPLICATION_KEY|${{ secrets.JWT_APPLICATION_KEY }}|" docker-compose.yml
sed -i "s|JWT_APPLICATION_EXPIRES_MINUTES|${{ vars.JWT_APPLICATION_EXPIRES_MINUTES }}|" docker-compose.yml
sed -i "s|JWT_APPLICATION_ISUSER|${{ vars.JWT_APPLICATION_ISUSER }}|" docker-compose.yml

sed -i "s|JWT_PASSWORD_KEY|${{ secrets.JWT_PASSWORD_KEY }}|" docker-compose.yml
sed -i "s|JWT_PASSWORD_EXPIRES_MINUTES|${{ vars.JWT_PASSWORD_EXPIRES_MINUTES }}|" docker-compose.yml
sed -i "s|JWT_PASSWORD_ISUSER|${{ vars.JWT_PASSWORD_ISUSER }}|" docker-compose.yml

sed -i "s|SENDGRID_KEY|${{ vars.SENDGRID_KEY }}|" docker-compose.yml
sed -i "s|SENDGRID_FORGOT_PASSWORD_ID|${{ vars.SENDGRID_FORGOT_PASSWORD_ID }}|" docker-compose.yml
sed -i "s|SENDGRID_FIRST_ACCESS_ID|${{ vars.SENDGRID_FIRST_ACCESS_ID }}|" docker-compose.yml
sed -i "s|URL_FRONT|${{ vars.URL_FRONT }}|" docker-compose.yml
sed -i "s|FROM_NAME|${{ vars.FROM_NAME }}|" docker-compose.yml
sed -i "s|FORM_EMAIL|${{ vars.FORM_EMAIL }}|" docker-compose.yml

sed -i "s|FILE_BUCKET_NAME|${{ vars.FILE_BUCKET_NAME }}|" docker-compose.yml
sed -i "s|FILE_BUCKET_URL|${{ vars.FILE_BUCKET_URL }}|" docker-compose.yml
sed -i "s|FILE_SERVER_URL|${{ vars.FILE_SERVER_URL }}|" docker-compose.yml
sed -i "s|FILE_KEY_ID|${{ secrets.FILE_KEY_ID }}|" docker-compose.yml
sed -i "s|FILE_SECRET_KEY|${{ secrets.FILE_SECRET_KEY }}|" docker-compose.yml

sed -i "s|DOCKER_SERVER_NAME|${{ vars.DOCKER_SERVER_NAME }}|" docker-compose.yml
sed -i "s|DOCKER_CONTAINER_NAME|${{ vars.DOCKER_CONTAINER_NAME }}|" docker-compose.yml
sed -i "s|DOCKER_IMAGE_NAME|felipemmachado28/boilerplate-dotnet8:${{vars.DOCKER_TAG }}|" docker-compose.yml
sed -i "s|DOCKER_PORT|${{ vars.DOCKER_PORT }}|" docker-compose.yml